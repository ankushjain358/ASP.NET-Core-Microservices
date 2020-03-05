using eShop.AccountService.DTO;

namespace eShop.AccountService.Service
{
    interface IAccountService
    {
        void RegisterUser(RegistrationRequestDTO registrationRequest);
        LoginResponseDTO Login(LoginRequestDTO loginRequest);
    }
}
