using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eShop.MobileRechargeService.DTO
{
    public class RechargeRequestDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Provider Id")]
        public int ProviderId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Invalid Recharge Amount")]
        public int Amount { get; set; }
    }
}
