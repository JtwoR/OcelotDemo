using Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Middleware
{
    public static class IdentityserverMiddleware
    {
        public static void AddIdentityserverMiddleware(this IServiceCollection services)
        {
            services.AddAuthentication("Bearer")
              .AddJwtBearer("Gateway", options =>
              {
                  options.Authority = GlobalConfig.IdentityserverUrl;
                  options.RequireHttpsMetadata = false;
                  options.Audience = "WebSite";
              });
        }

        public static void UseIdentityserverMiddleware(this IApplicationBuilder app)
        {
            app.UseAuthentication();//校验
        }
        
    }
}
