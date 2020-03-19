using AutoMapper;
using eShop.MobileRechargeService.DTO;
using eShop.MobileRechargeService.Repository;

namespace eShop.AccountService.Service
{
    public class DomainToDBProfile : Profile
    {
        public DomainToDBProfile()
        {
            CreateMap<RechargeRequestDTO, RechargeOrders>();
        }
    }
}
