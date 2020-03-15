using System;
using System.Collections.Generic;

namespace eShop.OrderService.Repository
{
    public partial class ServiceTypeEnumrations
    {
        public ServiceTypeEnumrations()
        {
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public string ServiceType { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}
