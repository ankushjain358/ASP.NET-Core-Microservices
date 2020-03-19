using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.Utilities.SharedDTO
{
    public class MobileRechargeMessageDTO
    {
        public int OrderId { get; set; }

        public int RechargeAmount { get; set; }

        public int RechargeProviderId { get; set; }
    }
}
