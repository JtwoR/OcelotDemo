using Common;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite_A.Middleware;

namespace WebSite_A
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc((option) =>
            {
                option.EnableEndpointRouting = false;
            });

            services.AddSingleton<IConsulClient>(c => new ConsulClient(client =>
            {
                client.Address = new Uri(GlobalConfig.ConsulserverUrl);
            }));

            //services.AddAuthentication("Bearer")
            //    .AddIdentityServerAuthentication(options =>
            //   {
            //       options.Authority = "http://localhost:6999";
            //       options.RequireHttpsMetadata = false;
            //       options.ApiName = "WebSite";
            //   });//单独授权校验

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app
            , IWebHostEnvironment env
            , IHostApplicationLifetime lifetime
            , IConsulClient client
            )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.RegisterConsul(client,lifetime);//consul注册接口
            //app.UseAuthentication();//配置中心授权校验
            app.UseMvc();

            
        }
    }
}
