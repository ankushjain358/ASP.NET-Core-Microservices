using eShop.Utilities.SharedDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.OrderService.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public ServiceType ServiceType { get; set; }
        public int? ExternalOrderId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
