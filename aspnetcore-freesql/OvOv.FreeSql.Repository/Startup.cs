using System.Diagnostics;
using System.Reflection;
using AutoMapper;
using FreeSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OvOv.FreeSql.Repository.Controllers;
using OvOv.FreeSql.Repository.Repositories;
using OvOv.FreeSql.Repository.Services;

namespace OvOv.FreeSql.Repository
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            IConfigurationSection configurationSection = Configuration.GetSection("Default");

            Fsql = new FreeSqlBuilder()
                .UseConnectionString(DataType.MySql, configurationSection.Value)
                .UseAutoSyncStructure(true)
                .UseMonitorCommand(cmd => Trace.WriteLine(cmd.CommandText))
                .Build();

            Fsql.CodeFirst.IsAutoSyncStructure = true;
        }

        public IFreeSql Fsql { get; }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddSingleton<IFreeSql>(Fsql);
            services.AddScoped<UnitOfWorkManager>();
            services.AddFreeRepository(null, typeof(Startup).Assembly);
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<BlogService>();
            services.AddScoped<TagService>();

            services.AddAutoMapper(Assembly.Load("OvOv.Core"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() { Title = "OvOv.FreeSql.Repository", Version = "v1" });
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OvOv.FreeSql.Repository");
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
