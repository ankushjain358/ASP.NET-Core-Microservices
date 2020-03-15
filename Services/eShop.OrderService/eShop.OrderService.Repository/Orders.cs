using System;
using System.Collections.Generic;

namespace eShop.OrderService.Repository
{
    public partial class Orders
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ServiceTypeId { get; set; }
        public int? ExternalOrderId { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedDate { get; set; }

        public ServiceTypeEnumrations ServiceType { get; set; }
        public StatusEnumrations Status { get; set; }
    }
}
