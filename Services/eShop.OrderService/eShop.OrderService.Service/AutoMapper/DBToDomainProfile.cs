using AutoMapper;
using eShop.OrderService.DTO;
using eShop.OrderService.Repository;
using eShop.Utilities.SharedDTO;

namespace eShop.OrderService.Service
{
    public class DBToDomainProfile : Profile
    {
        public DBToDomainProfile()
        {
            CreateMap<Orders, RechargeOrderResponseDTO>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => (OrderStatus)src.StatusId));

            CreateMap<Orders, OrderDTO>()
               .ForMember(dest => dest.ServiceType, opt => opt.MapFrom(src => (ServiceType)src.StatusId))
               .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => (OrderStatus)src.StatusId));
        }
    }
}
