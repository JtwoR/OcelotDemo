using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;
using OcelotDemo.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OcelotDemo
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

            services.AddSwagger();//≈‰÷√swagger

            services.AddAuthentication()
                    .AddJwtBearer("http",option=> {
                        option.Authority = "http://localhost:6999";
                        option.RequireHttpsMetadata = false;
                    });

            services
                .AddOcelot()
                .AddConsul()
                .AddPolly();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseSwaggerMiddleware();// π”√swagger

            
            
            app.UseMvc();
            app.UseOcelot().Wait();
        }
    }
}
