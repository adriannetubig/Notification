using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddNewtonsoftJson();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = Configuration.GetSection("Authentication:ValidIssuer").Value,
                    ValidAudience = Configuration.GetSection("Authentication:ValidAudience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("Authentication:IssuerSigningKey").Value)),
                    ClockSkew = TimeSpan.FromMinutes(Convert.ToDouble(Configuration.GetSection("Authentication:ClockSkewMinutes").Value))
                };
            });

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting(routes =>
            {
                routes.MapControllers();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:82")
                    .AllowAnyHeader()
                    .WithMethods("GET", "POST")
                    .AllowCredentials();
            });

            app.UseSignalR(routes =>
            {
                routes.MapHub<NotificationHub>("/notificationHub");
            });
        }
    }
}
