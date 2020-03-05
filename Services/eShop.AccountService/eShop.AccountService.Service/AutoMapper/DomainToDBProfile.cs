using AutoMapper;
using eShop.AccountService.DTO;
using eShop.AccountService.Repository;

namespace eShop.AccountService.Service
{
    public class DomainToDBProfile : Profile
    {
        public DomainToDBProfile()
        {
            CreateMap<RegistrationRequestDTO, Users>();
        }
    }
}
