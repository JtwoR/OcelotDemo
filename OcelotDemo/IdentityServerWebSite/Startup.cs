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

            //services.AddIdentityServer()
            //    .AddDeveloperSigningCredential()//��������(֤������)
            //    .AddInMemoryApiResources(IdentityServerConfig.ApiResource)
            //    .AddInMemoryApiScopes(IdentityServerConfig.ApiScope);

            var connection = "Server=localhost;Port=3306;User Id=root;Password=123456;Database=identityserver;Max Pool Size = 512;Connect Timeout=600000;charset=utf8mb4;SslMode=None;";

            var test=services.AddIdentityServer()
                .AddDeveloperSigningCredential()//��������(֤������)
                .AddInMemoryApiResources(new List<ApiResource>() { new ApiResource("WebSite") { } })
                .AddInMemoryApiScopes(new List<ApiScope>() { new ApiScope("WebSite") { } })
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseMySQL(connection,
                            sql => sql.MigrationsAssembly("IdentityServer"));
                });


            //var migrationsAssembly = typeof(Startup).GetType().Assembly.GetName().Name;
            

            //Add-Migration InitConfiguration -Context ConfigurationDbContext -o Date\Migrations\IdentityServer\ConfiguragtionDb
            //update-database
            //services.AddIdentityServer()
            //    .AddConfigurationStore(opt =>
            //    {
            //        opt.ConfigureDbContext = context =>
            //        {
            //            context.UseMySQL(connection, sql =>
            //            {
            //                sql.MigrationsAssembly("IdentityServer");
            //            });
            //        };
            //    });


            ////Add-Migration InitConfiguration -Context PersistedGrantDbContext -o Date\Migrations\IdentityServer\PersistedGrantDb
            ////update-database
            //services.AddIdentityServer()
            //   .AddConfigurationStore(opt =>
            //    {
            //        opt.ConfigureDbContext = context =>
            //        {
            //            context.UseMySQL(connection, sql =>
            //            {
            //                sql.MigrationsAssembly("IdentityServer");
            //            });
            //        };
            //    })
            //   .AddOperationalStore(opt =>
            //  {
            //      opt.ConfigureDbContext = context =>
            //      {
            //          context.UseMySQL(connection, sql =>
            //          {
            //              sql.MigrationsAssembly("IdentityServer");
            //          });
            //      };
            //      opt.EnableTokenCleanup = true;
            //      opt.TokenCleanupInterval = 30;
            //  });

            //// ���ݿ�����ϵͳӦ���û�����������
            //services.AddDbContext<ApplicationDbContext>(options => options.UseMySQL(connection));

            //// ���� Identity ���� ���ָ�����û��ͽ�ɫ���͵�Ĭ�ϱ�ʶϵͳ����
            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();

            app.UseMvc();
        }
    }

    // �����û����������ģ��̳� NetCore �Դ��� Identity ��֤���ƣ�Ҳ���Բ��̳ж��Զ����ṹ��
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
        //������������չ�����Ļ�˵��
    }



}
