using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
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
                .AddOcelot()
                .AddConsul()
                .AddPolly()
                .AddTransientDefinedAggregator<Aggregator.FakeDefinedAggregator>();
        }

        public static void UseOcelotMiddleware(this IApplicationBuilder app)
        {
            app.UseOcelot().Wait();
        }
    }
}
