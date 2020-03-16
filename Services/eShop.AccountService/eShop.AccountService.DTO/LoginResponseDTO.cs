using Newtonsoft.Json;
using System;

namespace eShop.AccountService.DTO
{
    public class LoginResponseDTO
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
    }
}
