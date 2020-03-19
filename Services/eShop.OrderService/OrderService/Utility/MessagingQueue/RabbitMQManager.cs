using eShop.OrderService.DTO;
using eShop.OrderService.Service;
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

namespace eShop.OrderService
{
    public class RabbitMQManager : IMessagingQueueManager
    {
        private readonly IConfiguration _configuration;
        private readonly IOrderService _orderService;

        private readonly string _username;
        private readonly string _password;
        private readonly string _hostname;
        private readonly string _exchangeName;
        private readonly string _mobileRechargeQueueName;
        private readonly string _mobileRechargeRoutingKey;
        private readonly string _orderStatusQueueName;
        private readonly string _orderStatusRoutingKey;


        public RabbitMQManager(IConfiguration configuration, IOrderService orderService)
        {
            _configuration = configuration;
            _orderService = orderService;

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

        public void PublishMessageForMobileRecharge(int orderId, int rechargeAmount, int rechargeProviderId)
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

                    MobileRechargeMessageDTO message = new MobileRechargeMessageDTO
                    {
                        OrderId = orderId,
                        RechargeAmount = rechargeAmount,
                        RechargeProviderId = rechargeProviderId
                    };

                    byte[] messagebuffer = Encoding.Default.GetBytes(JsonConvert.SerializeObject(message));

                    channel.BasicPublish(_exchangeName, _mobileRechargeRoutingKey, properties, messagebuffer);
                }
            }
        }


        public void InitializeSubescribers()
        {
            SubscribeForOrderStatusUpdate();
        }

        private void SubscribeForOrderStatusUpdate()
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
                OrderStatusMessageDTO orderStatus = JsonConvert.DeserializeObject<OrderStatusMessageDTO>(message);

                // 2. Update order status
                _orderService.UpdateOrderStatusWithExternalId(orderStatus);
            };
            channel.BasicConsume(queue: _orderStatusQueueName, autoAck: true, consumer: consumer);
        }
    }
}
