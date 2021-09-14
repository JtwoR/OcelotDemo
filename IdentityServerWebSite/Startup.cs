using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer.IdentityServer;
using IdentityServer4.EntityFramework.Mappers;

namespace IdentityServer
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

            //配置写到内存
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()//开发环境(证书问题)
                .AddInMemoryApiResources(IdentityServerConfig.ApiResource)//资源
                .AddInMemoryApiScopes(IdentityServerConfig.ApiScope)//范围
                .AddInMemoryClients(IdentityServerConfig.Client);//客户端

            #region 配置读取数据库数据
            //var connection = "Server=localhost;Port=3306;User Id=root;Password=123456;Database=identityserver;Max Pool Size = 512;Connect Timeout=600000;charset=utf8mb4;SslMode=None;";
            //services
            //    .AddIdentityServer()
            //    .AddDeveloperSigningCredential()
            //    .AddConfigurationStore(options =>
            //    {
            //        options.ConfigureDbContext = builder =>
            //        {
            //            builder.UseMySQL(connection,
            //                sql => sql.MigrationsAssembly("IdentityServer"));
            //        };
            //    })
            //.AddOperationalStore(options =>
            //{
            //    options.ConfigureDbContext = builder =>
            //    {
            //        builder.UseMySQL(connection,
            //            sql => sql.MigrationsAssembly("IdentityServer"));
            //    };
            //    options.EnableTokenCleanup = true;
            //});

            //var test=services.AddIdentityServer()
            //    .AddDeveloperSigningCredential()//开发环境(证书问题)
            //    .AddInMemoryApiResources(new List<ApiResource>() { new ApiResource("WebSite") { } })
            //    .AddInMemoryApiScopes(new List<ApiScope>() { new ApiScope("WebSite") { } })
            //    .AddConfigurationStore(options =>
            //    {
            //        options.ConfigureDbContext = builder =>
            //            builder.UseMySQL(connection,
            //                sql => sql.MigrationsAssembly("IdentityServer"));
            //    });


            //var migrationsAssembly = typeof(Startup).GetType().Assembly.GetName().Name;
            #endregion

            #region 授权中心数据库初始化
            //教程:https://www.cnblogs.com/chendongbky/p/12700339.html
            //官方文档:https://identityserver4.readthedocs.io/en/latest/quickstarts/5_entityframework.html?highlight=database

            //Add-Migration InitConfiguration -Context ConfigurationDbContext -o Date\Migrations\IdentityServer\ConfiguragtionDb
            //Update-database -context ConfigurationDbContext
            //按顺序把↓↓↓↓↓下面代码去除注释，IdentityServer项目设为启动项，使用nuget控制台执行↑↑↑↑↑↑ Add-Migration.....，后执行Update-database......
            //services.AddIdentityServer()
            //    .AddDeveloperSigningCredential()
            //    .AddConfigurationStore(opt =>
            //    {
            //        opt.ConfigureDbContext = context =>
            //        {
            //            context.UseMySQL(connection, sql =>
            //            {
            //                sql.MigrationsAssembly("IdentityServer");//项目编译后的dll名称
            //            });
            //        };

            //    });


            //Add-Migration InitConfiguration -Context PersistedGrantDbContext -o Date\Migrations\IdentityServer\PersistedGrantDb
            //Update-Database -Context PersistedGrantDbContext
            //按顺序把↓↓↓↓↓下面代码去除注释，IdentityServer项目设为启动项，使用nuget控制台执行↑↑↑↑↑↑ Add-Migration.....，后执行Update-database......
            //services.AddIdentityServer()
            //   .AddConfigurationStore(opt =>
            //    {
            //        opt.ConfigureDbContext = context =>
            //        {
            //            context.UseMySQL(connection, sql =>
            //            {
            //                sql.MigrationsAssembly("IdentityServer");//项目编译后的dll名称
            //            });
            //        };
            //    })
            //   .AddOperationalStore(opt =>
            //  {
            //      opt.ConfigureDbContext = context =>
            //      {
            //          context.UseMySQL(connection, sql =>
            //          {
            //              sql.MigrationsAssembly("IdentityServer");//项目编译后的dll名称
            //          });
            //      };
            //      opt.EnableTokenCleanup = true;
            //      opt.TokenCleanupInterval = 30;
            //  });

            //Update-Database -Context ApplicationDbContext
            // 数据库配置系统应用用户数据上下文
            //按顺序把↓↓↓↓↓下面代码去除注释，IdentityServer项目设为启动项，使用nuget控制台执行↑↑↑↑↑↑ 执行Update-database......
            //services.AddDbContext<ApplicationDbContext>(options => options.UseMySQL(connection));

            //// 启用 Identity 服务 添加指定的用户和角色类型的默认标识系统配置
            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //InitIdentityServerDataBase(app);//导入数据库配置
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();

            app.UseMvc();
        }

        /// <summary>
        /// 导入数据库配置
        /// </summary>
        /// <param name="app"></param>
        public void InitIdentityServerDataBase(IApplicationBuilder app)
        {
            //ApplicationServices返回的就是IServiceProvider，依赖注入的容器
            using (var scope = app.ApplicationServices.CreateScope())
            {
                //Update-Database
                scope.ServiceProvider.GetService<IdentityServer4.EntityFramework.DbContexts.PersistedGrantDbContext>().Database.Migrate();

                //var provide = scope.ServiceProvider.GetService<PersistedGrantDbContext>();
                //ckk.PersistedGrants.Add(new IdentityServer4.EntityFramework.Entities.PersistedGrant {

                //});

                var configurationDbContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

                /*
                 如果不走这个，
                 那么应该手动执行 Update-Database -Context PersistedGrantDbContext
                 */
                configurationDbContext.Database.Migrate();

                if (!configurationDbContext.Clients.Any())
                {
                    foreach (var client in IdentityServerConfig.Client)
                    {
                        //client.ToEntity() 会把当前实体映射到EF实体
                        configurationDbContext.Clients.Add(client.ToEntity());
                    }
                    configurationDbContext.SaveChanges();
                }
                if (!configurationDbContext.ApiResources.Any())
                {
                    foreach (var api in IdentityServerConfig.ApiResource)
                    {
                        configurationDbContext.ApiResources.Add(api.ToEntity());
                    }
                    configurationDbContext.SaveChanges();
                }
                if (!configurationDbContext.IdentityResources.Any())
                {
                    foreach (var identity in IdentityServerConfig.ApiScope)
                    {
                        configurationDbContext.ApiScopes.Add(identity.ToEntity());
                    }
                    configurationDbContext.SaveChanges();
                }
            }
        }
    }
}


    // 定义用户管理上下文，继承 NetCore 自带的 Identity 认证机制，也可以不继承而自定义表结构。
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                    : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }

    public class ApplicationUser : IdentityUser
    {
        //可以在这里扩展，下文会说到
    }



