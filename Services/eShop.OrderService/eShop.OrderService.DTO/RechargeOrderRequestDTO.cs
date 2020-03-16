using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eShop.OrderService.DTO
{
    public class RechargeOrderRequestDTO
    {
        public int UserId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Recharge Provider Id")]
        public int RechargeProviderId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Recharge Amount")]
        public int RechargeAmount { get; set; }
    }
}
