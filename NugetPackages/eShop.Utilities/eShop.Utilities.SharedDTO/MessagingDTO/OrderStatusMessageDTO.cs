using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.Utilities.SharedDTO
{
    public class OrderStatusMessageDTO
    {
        public int OrderId { get; set; }
        public int RechargeOrderId { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
