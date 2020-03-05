using AutoMapper;
using eShop.AccountService.DTO;
using eShop.AccountService.Repository;

namespace eShop.AccountService.Service
{
    public class DBToDomainProfile : Profile
    {
        public DBToDomainProfile()
        {
            CreateMap<Users, LoginResponseDTO>();
        }
    }
}
