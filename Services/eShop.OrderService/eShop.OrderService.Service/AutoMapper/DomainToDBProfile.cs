using AutoMapper;
using eShop.OrderService.DTO;
using eShop.OrderService.Repository;
using eShop.Utilities.SharedDTO;
using System;

namespace eShop.OrderService.Service
{
    public class DomainToDBProfile : Profile
    {
        public DomainToDBProfile()
        {
            CreateMap<RechargeOrderRequestDTO, Orders>()
                .ForMember(dest => dest.ServiceTypeId, opt => Convert.ToInt32(ServiceType.MobileRechargeService))
                .AfterMap((src, dest) =>
                {
                    dest.ServiceTypeId = Convert.ToInt32(ServiceType.MobileRechargeService);
                    dest.StatusId = Convert.ToInt32(OrderStatus.Pending);
                    dest.CreatedDate = DateTime.Now;
                });
        }
    }
}
