using System;
using System.Text;
using AuthenticationFunction;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc()
            //    .AddNewtonsoftJson();

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


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors(options =>
            {
                options.AddPolicy("CORS", corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddScoped<IFAuthentication, FAuthentication>();
            services.AddScoped<IFRefreshToken, FRefreshToken>();
            services.AddScoped<IFUser, FUser>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseRouting(routes =>
            //{
            //    routes.MapControllers();
            //});

            app.UseAuthentication();
            //app.UseAuthorization();

            //var allowedOrigins = Configuration.GetSection("Cors:Origins").Get<string[]>();
            //app.UseCors(builder =>
            //{
            //    builder.WithOrigins(allowedOrigins)
            //        .AllowAnyHeader()
            //        .WithMethods("GET", "POST")
            //        .AllowCredentials();
            //});

            app.UseCors("CORS");

            app.UseMvc();
        }
    }
}
