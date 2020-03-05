using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace eShop.AccountService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

// TODO:
// Features: 
// 1. Layered Architecture [Done]
// 2. Validations [Done]
// 3. Dependency Injection 
// 4. Uniform Response [Done]
// 5. Entity Framework Core - Database First [Done]
// 6. Generic Repository Pattern [Done]
// 7. Containerizable [Done]
// 8. AutoMapper [Done]
// 9. Middleware [Done]
// 10. Filters [Done]
// 11. Access Token [Pending]