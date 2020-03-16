using eShop.AccountService.DTO;
using eShop.AccountService.Service;
using eShop.Utilities.WebApiUtility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace eShop.AccountService.API.Controllers
{
    [Route("api/[controller]")]
    [ProducesResponseType(500, Type = typeof(BaseResponse))]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<LoginResponseDTO>))]
        public ApiResponse<LoginResponseDTO> Login(LoginRequestDTO request)
        {
            LoginResponseDTO response = _accountService.Login(request);
            if (response != null)
            {
                response.AccessToken = new TokenGenerator(_configuration).GenerateJSONWebToken(response.UserId, response.Email);
                return new ApiResponse<LoginResponseDTO>(response);
            }
            return new ApiResponse<LoginResponseDTO>("Login Failed", null, false);
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(200, Type = typeof(BaseResponse))]
        public BaseResponse Register(RegistrationRequestDTO request)
        {
            _accountService.RegisterUser(request);

            return new BaseResponse { Success = true };
        }
    }
}