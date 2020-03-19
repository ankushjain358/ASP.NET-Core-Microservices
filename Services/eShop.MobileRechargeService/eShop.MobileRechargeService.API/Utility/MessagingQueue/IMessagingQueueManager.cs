using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.MobileRechargeService
{
    public interface IMessagingQueueManager
    {
        void InitializeSubescribers();
    }
}
