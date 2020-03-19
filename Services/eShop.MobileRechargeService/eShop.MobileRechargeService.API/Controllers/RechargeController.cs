using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eShop.MobileRechargeService.Service;
using eShop.Utilities.WebApiUtility;
using eShop.MobileRechargeService.DTO;
using Microsoft.AspNetCore.Authorization;

namespace MobileRechargeService.Controllers
{
    [Route("api/[controller]")]
    [ProducesResponseType(500, Type = typeof(BaseResponse))]
    [ApiController]
    [Authorize]
    public class RechargeController : ControllerBase
    {
        private readonly IMobileRechargeService _mobileRechargeService;

        public RechargeController(IMobileRechargeService mobileRechargeService)
        {
            _mobileRechargeService = mobileRechargeService;
        }

        [HttpPost]
        [Route("get-providers")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<ProviderDTO>>))]
        public ApiResponse<List<ProviderDTO>> GetProviders()
        {
            List<ProviderDTO> providerList = _mobileRechargeService.GetProviderList();
            return new ApiResponse<List<ProviderDTO>>(providerList);
        }

        [HttpPost]
        [Route("get-recharge-detail/{id}")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<RechargeDetailResponseDTO>))]
        public ApiResponse<RechargeDetailResponseDTO> GetRechargeDetail(int rechargeOrderId)
        {
            RechargeDetailResponseDTO rechargeDetail = _mobileRechargeService.GetRechargeDetail(rechargeOrderId);
            return new ApiResponse<RechargeDetailResponseDTO>(rechargeDetail);
        }
    }
}
