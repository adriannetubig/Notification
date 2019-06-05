using System;
using System.Text;
using AuthenticationFunction;
using AuthenticationModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var authorization = Configuration.GetSection("Authorization").Get<Authorization>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = authorization.ValidIssuer,
                    ValidAudience = authorization.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authorization.IssuerSigningKey)),
                    ClockSkew = authorization.ClockSkew
                };
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors(options =>
            {
                options.AddPolicy("CORS", corsPolicyBuilder => corsPolicyBuilder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins(authorization.AllowedOrigins)
                    .AllowCredentials());
            });

            services.AddScoped<IFAuthentication>(a => new FAuthentication(authorization));
            services.AddScoped<IFRefreshToken, FRefreshToken>();
            services.AddScoped<IFUser, FUser>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseAuthentication();

            app.UseCors("CORS");

            app.UseMvc();
        }
    }
}
