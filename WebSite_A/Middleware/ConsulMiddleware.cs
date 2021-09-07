using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite_A.Middleware
{
    public static class ConsulMiddleware
    {

        private static string _serviceName = "ConsulTest";

        public static IApplicationBuilder RegisterConsul(this IApplicationBuilder app, IConsulClient client, IHostApplicationLifetime lifetime)
        {
            
            var httpCheck = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务启动多久后注册
                Interval = TimeSpan.FromSeconds(1),//健康检查时间间隔，或者称为心跳间隔
                HTTP = $"{Program.siteUrl}/api/values/health",//健康检查地址
                Timeout = TimeSpan.FromSeconds(5)
            };

            string ID = Guid.NewGuid().ToString();
            // Register service with consul
            var registration = new AgentServiceRegistration()
            {
                Checks = new[] { httpCheck },
                ID = ID,
                Name = _serviceName,
                Address = Program.siteInfo.Host,
                Port = Program.siteInfo.Port,
                Tags = new[] { $"WebSite_A" }
            };

            client.Agent.ServiceRegister(registration).Wait();//服务启动时注册
            lifetime.ApplicationStopping.Register(() =>
            {
                client.Agent.ServiceDeregister(registration.ID).Wait();//服务停止时取消注册
            });

            return app;
        }

    }
}
