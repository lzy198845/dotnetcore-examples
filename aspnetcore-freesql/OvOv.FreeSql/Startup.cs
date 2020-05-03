﻿using System.Reflection;
using AutoMapper;
using FreeSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace OvOv.FreeSql
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Fsql = new FreeSqlBuilder()
                .UseConnectionString(DataType.MySql, @"Data Source=127.0.0.1;Port=3306;User ID=root;Password=123456;Initial Catalog=OvOv_FreeSql;Charset=utf8;SslMode=none;Max pool size=10")
                .UseAutoSyncStructure(true)
                .Build();

            Fsql.CodeFirst.IsAutoSyncStructure = true;

       
        }
        public IConfiguration Configuration { get; }
        public IFreeSql Fsql { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IFreeSql>(Fsql);

            //AddAutoMapper会去找继承Profile的类，这个只适用于继承Profile类在当前项目。
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //OvOv.Web.Core 字符串为项目名
            services.AddAutoMapper(Assembly.Load("OvOv.Core"));
            //或某一个类所在程序集
            //services.AddAutoMapper(typeof(Blog).Assembly);

            services.AddControllersWithViews();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() { Title = "OvOv.FreeSql", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OvOv.FreeSql");
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