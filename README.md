# OcelotDemo

* ### 使用到中间件：
    > 1、Ocelot 网关\
    > 2、Consul 服务发现\
    > 3、IdentityServer 授权中心 

* ### 说明
    ---
    授权中心地址 http://localhost:6999 \
    网关地址 http://localhost:7770 \
    下游站点A地址 http://localhost:7771 \
    下游站点B地址 http://localhost:7772 

    ---

    ### 当前Demo默认开启授权校验，如需Consul功能调试需要把以下注释去掉，且准备好ConsulServer
    \
    OcelotMiddleware.cs的AddConsul()
    ```c#
    public static void AddOcelotMiddleware(this IServiceCollection services) {
        services
            .AddOcelot()//网关
            .AddConsul()//服务发现
            .AddPolly()//限流熔断
            .AddCacheManager(x=>x.WithDictionaryHandle())//缓存
            .AddTransientDefinedAggregator<Aggregator.FakeDefinedAggregator>();//聚合请求
    }
    ```
    站点A、B的Startup.cs
    ```c#
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
    ```

    ---

* 授权中心获取token

    >Post请求\
     http://localhost:6999/connect/token \
     body使用x-www-form_urlencoded
    
    >参数：\
    client_id:client\
    client_secret:secret\
    grant_type:client_credentials\
    scope:WebSite

