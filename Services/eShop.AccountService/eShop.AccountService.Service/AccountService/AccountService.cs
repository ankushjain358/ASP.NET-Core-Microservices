using eShop.AccountService.DTO;
using eShop.AccountService.Repository;
using AutoMapper;
using System.Linq;

namespace eShop.AccountService.Service
{
    public class AccountService : IAccountService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public LoginResponseDTO Login(LoginRequestDTO loginRequest)
        {
            var userEntity = _unitOfWork.UserRepository.Get(item => item.Email == loginRequest.Email && item.Password == loginRequest.Password).FirstOrDefault();
            return _mapper.Map<LoginResponseDTO>(userEntity);
        }

        public void RegisterUser(RegistrationRequestDTO registrationRequest)
        {
            var userEntity = _mapper.Map<Users>(registrationRequest);
            _unitOfWork.UserRepository.Insert(userEntity);
            _unitOfWork.SaveChanges();
        }
    
    }
}
