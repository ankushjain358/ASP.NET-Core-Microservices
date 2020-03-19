using eShop.MobileRechargeService.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.MobileRechargeService.Service
{
    public interface IMobileRechargeService
    {
        void DoRecharge(RechargeRequestDTO request, out int rechargeOrderId);
        RechargeDetailResponseDTO GetRechargeDetail(int rechargeOrderId);
        List<ProviderDTO> GetProviderList();
    }
}
