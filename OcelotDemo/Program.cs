using Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((app) => { app.AddJsonFile("Configs/ocelot.json"); });//Ìí¼ÓÅäÖÃÎÄ¼ş

                    webBuilder.UseUrls($"http://{GlobalConfig.Host}:7770");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
