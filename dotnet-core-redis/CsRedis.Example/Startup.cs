﻿using CSRedis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace CsRedis.Example
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // eg 1.单个redis实现 普通模式
            //CSRedisClient csredis = new CSRedisClient("127.0.0.1:6379,password=,defaultDatabase=csredis,prefix=csredis-example");

            //IConfigurationSection configurationSection = Configuration.GetSection("CsRedisConfig:DefaultConnectString");
            //eg 2.单个redis，使用appsettings.json中的配置项
            //CSRedisClient csredis = new CSRedisClient(configurationSection.Value);
            //appsettings.json
            //"CsRedisConfig": {
            //    "CsRedisConnectString": "mymaster,127.0.0.1:6379,password=,defaultDatabase=csredis,prefix=csredis-example"
            //}

            //eg.3 使用appsettings.json,哨兵模式
            IConfigurationSection configurationSection = Configuration.GetSection("CsRedisConfig:SentinelConnectString");

            string[] sentinelValues = Configuration.GetSection("CsRedisConfig:Sentinel").Get<string[]>();

            CSRedisClient csredis = new CSRedisClient(configurationSection.Value, sentinelValues);

            //初始化 RedisHelper
            RedisHelper.Initialization(csredis);
            //注册mvc分布式缓存
            services.AddSingleton<IDistributedCache>(new CSRedisCache(RedisHelper.Instance));

            services.AddControllersWithViews();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo(){ Title = "CsRedis.Example", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment  env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Webapi.FreeSql");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
