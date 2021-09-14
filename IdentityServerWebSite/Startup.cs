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

            //����д���ڴ�
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()//��������(֤������)
                .AddInMemoryApiResources(IdentityServerConfig.ApiResource)//��Դ
                .AddInMemoryApiScopes(IdentityServerConfig.ApiScope)//��Χ
                .AddInMemoryClients(IdentityServerConfig.Client);//�ͻ���

            #region ���ö�ȡ���ݿ�����
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
            //    .AddDeveloperSigningCredential()//��������(֤������)
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

            #region ��Ȩ�������ݿ��ʼ��
            //�̳�:https://www.cnblogs.com/chendongbky/p/12700339.html
            //�ٷ��ĵ�:https://identityserver4.readthedocs.io/en/latest/quickstarts/5_entityframework.html?highlight=database

            //Add-Migration InitConfiguration -Context ConfigurationDbContext -o Date\Migrations\IdentityServer\ConfiguragtionDb
            //Update-database -context ConfigurationDbContext
            //��˳��ѡ����������������ȥ��ע�ͣ�IdentityServer��Ŀ��Ϊ�����ʹ��nuget����ִ̨�С����������� Add-Migration.....����ִ��Update-database......
            //services.AddIdentityServer()
            //    .AddDeveloperSigningCredential()
            //    .AddConfigurationStore(opt =>
            //    {
            //        opt.ConfigureDbContext = context =>
            //        {
            //            context.UseMySQL(connection, sql =>
            //            {
            //                sql.MigrationsAssembly("IdentityServer");//��Ŀ������dll����
            //            });
            //        };

            //    });


            //Add-Migration InitConfiguration -Context PersistedGrantDbContext -o Date\Migrations\IdentityServer\PersistedGrantDb
            //Update-Database -Context PersistedGrantDbContext
            //��˳��ѡ����������������ȥ��ע�ͣ�IdentityServer��Ŀ��Ϊ�����ʹ��nuget����ִ̨�С����������� Add-Migration.....����ִ��Update-database......
            //services.AddIdentityServer()
            //   .AddConfigurationStore(opt =>
            //    {
            //        opt.ConfigureDbContext = context =>
            //        {
            //            context.UseMySQL(connection, sql =>
            //            {
            //                sql.MigrationsAssembly("IdentityServer");//��Ŀ������dll����
            //            });
            //        };
            //    })
            //   .AddOperationalStore(opt =>
            //  {
            //      opt.ConfigureDbContext = context =>
            //      {
            //          context.UseMySQL(connection, sql =>
            //          {
            //              sql.MigrationsAssembly("IdentityServer");//��Ŀ������dll����
            //          });
            //      };
            //      opt.EnableTokenCleanup = true;
            //      opt.TokenCleanupInterval = 30;
            //  });

            //Update-Database -Context ApplicationDbContext
            // ���ݿ�����ϵͳӦ���û�����������
            //��˳��ѡ����������������ȥ��ע�ͣ�IdentityServer��Ŀ��Ϊ�����ʹ��nuget����ִ̨�С����������� ִ��Update-database......
            //services.AddDbContext<ApplicationDbContext>(options => options.UseMySQL(connection));

            //// ���� Identity ���� ���ָ�����û��ͽ�ɫ���͵�Ĭ�ϱ�ʶϵͳ����
            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //InitIdentityServerDataBase(app);//�������ݿ�����
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();

            app.UseMvc();
        }

        /// <summary>
        /// �������ݿ�����
        /// </summary>
        /// <param name="app"></param>
        public void InitIdentityServerDataBase(IApplicationBuilder app)
        {
            //ApplicationServices���صľ���IServiceProvider������ע�������
            using (var scope = app.ApplicationServices.CreateScope())
            {
                //Update-Database
                scope.ServiceProvider.GetService<IdentityServer4.EntityFramework.DbContexts.PersistedGrantDbContext>().Database.Migrate();

                //var provide = scope.ServiceProvider.GetService<PersistedGrantDbContext>();
                //ckk.PersistedGrants.Add(new IdentityServer4.EntityFramework.Entities.PersistedGrant {

                //});

                var configurationDbContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

                /*
                 ������������
                 ��ôӦ���ֶ�ִ�� Update-Database -Context PersistedGrantDbContext
                 */
                configurationDbContext.Database.Migrate();

                if (!configurationDbContext.Clients.Any())
                {
                    foreach (var client in IdentityServerConfig.Client)
                    {
                        //client.ToEntity() ��ѵ�ǰʵ��ӳ�䵽EFʵ��
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



