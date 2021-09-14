using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Middleware
{
    public static class OcelotMiddleware
    {
        public static void AddOcelotMiddleware(this IServiceCollection services) {

            services
                .AddOcelot()//网关
                .AddConsul()//服务发现
                .AddPolly()//限流熔断
                .AddCacheManager(x=>x.WithDictionaryHandle())//缓存
                .AddTransientDefinedAggregator<Aggregator.FakeDefinedAggregator>();//聚合请求
        }

        public static void UseOcelotMiddleware(this IApplicationBuilder app)
        {
            app.UseOcelot().Wait();
        }
    }
}
