using eShop.OrderService.DTO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.OrderService
{
    public interface IMessagingQueueManager
    {
        void PublishMessageForMobileRecharge(int orderId, int rechargeAmount, int rechargeProviderId);
        void InitializeSubescribers();
    }
}
