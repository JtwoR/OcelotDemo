using Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite_B
{
    public class Program
    {
        public static dynamic siteInfo = new
        {
            Scheme = "http",
            Host = GlobalConfig.Host,
            Port = 7772
        };

        public static string siteUrl = $"{siteInfo.Scheme}://{siteInfo.Host}:{siteInfo.Port}";

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls(siteUrl);
                    webBuilder.UseStartup<Startup>();
                });
    }
}
