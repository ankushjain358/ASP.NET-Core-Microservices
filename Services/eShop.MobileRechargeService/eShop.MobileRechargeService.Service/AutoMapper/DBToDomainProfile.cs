using AutoMapper;
using eShop.MobileRechargeService.DTO;
using eShop.MobileRechargeService.Repository;

namespace eShop.AccountService.Service
{
    public class DBToDomainProfile : Profile
    {
        public DBToDomainProfile()
        {
            CreateMap<Providers, ProviderDTO>();
            CreateMap<RechargeOrders, RechargeDetailResponseDTO>();
        }
    }
}
