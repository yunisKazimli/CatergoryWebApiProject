using CatergoryWebApiProject.DataTableManagment;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatergoryWebApiProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SqlConnector.Init(@"Server = DESKTOP-IAADCGV\SQLSERVER; Database = CategoryDataBase; Trusted_Connection = True; TrustServerCertificate = True; ");

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
