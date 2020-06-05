using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using AutoMapper;
using FreeSql;
using FreeSql.Internal;
using FreeSql.Internal.ObjectPool;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OvOv.Core.Domain;
using OvOv.FreeSql.AutoFac.DynamicProxy.Repositories;
using OvOv.FreeSql.AutoFac.DynamicProxy.Services;

namespace OvOv.FreeSql.AutoFac.DynamicProxy
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            IConfigurationSection configurationSection = Configuration.GetSection("Default");

            Fsql = new FreeSqlBuilder()
                //.UseConnectionString(DataType.Sqlite, @"Data Source=|DataDirectory|\document.db;Pooling=true;Max Pool Size=10")
                .UseConnectionString(DataType.MySql, configurationSection.Value)
                .UseAutoSyncStructure(true)
                .UseNameConvert(NameConvertType.PascalCaseToUnderscoreWithLower)
                .UseMonitorCommand(cmd => Trace.WriteLine(cmd.CommandText))
                .Build().SetDbContextOptions(opt => opt.EnableAddOrUpdateNavigateList = true);

            Fsql.CodeFirst.IsAutoSyncStructure = true;


            Fsql.Aop.CurdAfter += (s, e) =>
            {
                Trace.WriteLine(
                    $"ManagedThreadId:{Thread.CurrentThread.ManagedThreadId}: FullName:{e.EntityType.FullName}" +
                    $" ElapsedMilliseconds:{e.ElapsedMilliseconds}ms, {e.Sql}");

                if (e.ElapsedMilliseconds > 200)
                {
                }
            };

            using Object<DbConnection> objPool = Fsql.Ado.MasterPool.Get();

            DbConnection dbConnection = objPool.Value;

        }

        public IFreeSql Fsql { get; }
        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Fsql);
            services.AddScoped<UnitOfWorkManager>();
            this.AddFreeRepository(services);

            Expression<Func<IDeleteAduitEntity, bool>> where = a => a.IsDeleted == false;
            Fsql.GlobalFilter.Apply("IsDeleted", where);

            services.AddControllersWithViews();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();

            services.AddAutoMapper(Assembly.Load("OvOv.Core"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() { Title = "OvOv.FreeSql.Autofac.DynamicProxy", Version = "v1" });
            });
        }

        public IServiceCollection AddFreeRepository(IServiceCollection services)
        {
            services.TryAddTransient(typeof(IBaseRepository<>), typeof(GuidRepository<>));
            services.TryAddTransient(typeof(BaseRepository<>), typeof(GuidRepository<>));
            services.TryAddTransient(typeof(IBaseRepository<,>), typeof(DefaultRepository<,>));
            services.TryAddTransient(typeof(BaseRepository<,>), typeof(DefaultRepository<,>));
            return services;
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule());
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OvOv.FreeSql.Autofac.DynamicProxy");
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