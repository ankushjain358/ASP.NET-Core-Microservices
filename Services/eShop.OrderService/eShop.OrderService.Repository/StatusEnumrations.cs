using System;
using System.Collections.Generic;

namespace eShop.OrderService.Repository
{
    public partial class StatusEnumrations
    {
        public StatusEnumrations()
        {
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public string Status { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}
