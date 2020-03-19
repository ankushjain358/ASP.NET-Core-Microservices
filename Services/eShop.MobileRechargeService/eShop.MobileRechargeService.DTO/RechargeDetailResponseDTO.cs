using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.MobileRechargeService.DTO
{
    public class RechargeDetailResponseDTO
    {
        public int RechargeOrderId { get; set; }
        public string ProviderName { get; set; }
        public int RechargeAmount { get; set; }
    }
}
