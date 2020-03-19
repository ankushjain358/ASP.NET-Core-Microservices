using System;
using System.Collections.Generic;

namespace eShop.MobileRechargeService.Repository
{
    public partial class RechargeOrders
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public int Amount { get; set; }
    }
}
