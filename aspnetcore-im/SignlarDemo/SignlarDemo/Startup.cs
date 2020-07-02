using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace SignlarDemo
{
    public class NameUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }

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
            services.AddHash(10000, 128);
            services.AddCryptography("lin-cms-dotnetcore-cryptography");
            services.AddJsonWebToken(
                new JsonWebTokenSettings(
                        Configuration["Authentication:JwtBearer:SecurityKey"],
                        new TimeSpan(30, 0, 0, 0),
                        Configuration["Authentication:JwtBearer:Audience"],
                        Configuration["Authentication:JwtBearer:Issuer"]
                    )
                );
            var jsonWebTokenSettings = services.BuildServiceProvider().GetRequiredService<JsonWebTokenSettings>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      // The signing key must match!
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jsonWebTokenSettings.Key)),

                      // Validate the JWT Issuer (iss) claim
                      ValidateIssuer = true,
                      ValidIssuer = jsonWebTokenSettings.Issuer,

                      // Validate the JWT Audience (aud) claim
                      ValidateAudience = true,
                      ValidAudience = jsonWebTokenSettings.Audience,

                      // Validate the token expiry
                      ValidateLifetime = true,

                  };
                  options.Events = new JwtBearerEvents
                  {
                      OnMessageReceived = context =>
                      {
                          var accessToken = context.Request.Query["access_token"];

                      // If the request is for our hub...
                      var path = context.HttpContext.Request.Path;
                          if (!string.IsNullOrEmpty(accessToken) &&
                              (path.StartsWithSegments("/chatHub")))
                          {
                          // Read the token out of the query string
                          context.Token = accessToken;
                          }
                          return Task.CompletedTask;
                      }
                  };
              });
            services.AddSingleton<IUserIdProvider, NameUserIdProvider>();
            //ע��SignalRʵʱͨѶ��Ĭ����json����
            services.AddSignalR(options =>
            {
                //�ͻ��˷������������󵽷����������Ĭ��30�룬�ĳ�4���ӣ���ҳ���������connection.keepAliveIntervalInMilliseconds = 12e4;��2����
                options.ClientTimeoutInterval = TimeSpan.FromMinutes(4);
                //����˷������������󵽿ͻ��˼����Ĭ��15�룬�ĳ�2���ӣ���ҳ���������connection.serverTimeoutInMilliseconds = 24e4;��4����
                options.KeepAliveInterval = TimeSpan.FromMinutes(2);
            });

            //services.AddHostedService<DashboardHostedService>();
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //���WebSocket֧�֣�SignalR����ʹ��WebSocket����
            app.UseWebSockets();

            app.Use(async (context, next) =>
            {
                IHubContext<MessageHub> hubContext = context.RequestServices.GetRequiredService<IHubContext<MessageHub>>();

                //await hubContext.Clients.All.SendAsync("ReloadPage", DateTime.Now);
                if (next != null)
                {
                    await next.Invoke();
                }
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapHub<MessageHub>("/chatHub");
            });
        }
    }
}
