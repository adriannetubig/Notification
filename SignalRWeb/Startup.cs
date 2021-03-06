using System;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SignalRFunction;
using SignalRWeb.Hubs;

namespace SignalRWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = Configuration.GetSection("Authorization:ValidIssuer").Value,
                    ValidAudience = Configuration.GetSection("Authorization:ValidAudience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("Authorization:IssuerSigningKey").Value)),
                    ClockSkew = TimeSpan.FromMinutes(Convert.ToDouble(Configuration.GetSection("Authorization:ClockSkewMinutes").Value))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        if (!string.IsNullOrEmpty(accessToken) &&
                            (context.HttpContext.WebSockets.IsWebSocketRequest || context.Request.Headers["Accept"] == "text/event-stream"))
                        {
                            context.Token = context.Request.Query["access_token"];
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors(options =>
            {
                options.AddPolicy("CORS", corsPolicyBuilder => corsPolicyBuilder
                    .AllowAnyMethod()
                    .WithOrigins(Configuration.GetSection("Authorization:AllowedOrigins").Get<string[]>())
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddScoped<IFNotification, FNotification>();

            services.AddSignalR();
            services.AddMediatR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseCors("CORS");

            app.UseSignalR(routes =>
            {
                routes.MapHub<NotificationHub>("/notificationHub");
                routes.MapHub<AuthenticatedHub>("/authenticatedHub");
                routes.MapHub<UnauthenticatedHub>("/unauthenticatedHub");
            });
            app.UseMvc();
        }
    }
}
