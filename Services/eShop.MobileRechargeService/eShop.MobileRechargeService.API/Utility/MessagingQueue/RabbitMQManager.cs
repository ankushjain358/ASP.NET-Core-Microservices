using eShop.MobileRechargeService.DTO;
using eShop.MobileRechargeService.Service;
using eShop.Utilities.SharedDTO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.MobileRechargeService
{
    public class RabbitMQManager : IMessagingQueueManager
    {
        private readonly IConfiguration _configuration;
        private readonly IMobileRechargeService _mobileRechargeService;

        private readonly string _username;
        private readonly string _password;
        private readonly string _hostname;
        private readonly string _exchangeName;
        private readonly string _mobileRechargeQueueName;
        private readonly string _mobileRechargeRoutingKey;
        private readonly string _orderStatusQueueName;
        private readonly string _orderStatusRoutingKey;


        public RabbitMQManager(IConfiguration configuration, IMobileRechargeService mobileRechargeService)
        {
            _configuration = configuration;
            _mobileRechargeService = mobileRechargeService;

            _username = _configuration["RabbitMQ:Username"];
            _password = _configuration["RabbitMQ:Password"];
            _hostname = _configuration["RabbitMQ:Hostname"];

            var eShopExchange = _configuration.GetSection("RabbitMQ:Exchanges").GetChildren().First();

            _exchangeName = eShopExchange["Name"];
            _mobileRechargeQueueName = eShopExchange["Queues:MobileRechargeQueue:Name"];
            _mobileRechargeRoutingKey = eShopExchange["Queues:MobileRechargeQueue:RoutingKey"];
            _orderStatusQueueName = eShopExchange["Queues:OrderStatusQueue:Name"];
            _orderStatusRoutingKey = eShopExchange["Queues:OrderStatusQueue:RoutingKey"];
        }

        public void InitializeSubescribers()
        {
            SubscribeForMobileRechargeUpdates();
        }

        private void SubscribeForMobileRechargeUpdates()
        {
            var connectionFactory = new ConnectionFactory()
            {
                UserName = _username,
                Password = _password,
                HostName = _hostname
            };

            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);

                // 1. Get the message
                MobileRechargeMessageDTO messageDTO = JsonConvert.DeserializeObject<MobileRechargeMessageDTO>(message);

                // 2. Do the recharge
                _mobileRechargeService.DoRecharge(new RechargeRequestDTO
                {
                    ProviderId = messageDTO.RechargeProviderId,
                    Amount = messageDTO.RechargeAmount
                }, out int rechargeOrderId);

                PublishMessageForOrderStatus(new OrderStatusMessageDTO { OrderId = messageDTO.OrderId, RechargeOrderId = rechargeOrderId, OrderStatus = OrderStatus.Success });
            };
            channel.BasicConsume(queue: _mobileRechargeQueueName, autoAck: true, consumer: consumer);
        }

        private void PublishMessageForOrderStatus(OrderStatusMessageDTO message)
        {
            var connectionFactory = new ConnectionFactory()
            {
                UserName = _username,
                Password = _password,
                HostName = _hostname
            };

            using (var connection = connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = false;

                    byte[] messagebuffer = Encoding.Default.GetBytes(JsonConvert.SerializeObject(message));

                    channel.BasicPublish(_exchangeName, _orderStatusRoutingKey, properties, messagebuffer);
                }
            }
        }
    }
}
