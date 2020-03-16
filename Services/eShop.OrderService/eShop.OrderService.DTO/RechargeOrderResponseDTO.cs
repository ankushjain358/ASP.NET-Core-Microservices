using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.OrderService.DTO
{
    public class RechargeOrderResponseDTO
    {
        public int OrderId { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
