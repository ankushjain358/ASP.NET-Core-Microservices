using System;
using System.Collections.Generic;

namespace eShop.AccountService.Repository
{
    public partial class Users
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
