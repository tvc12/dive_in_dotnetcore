using System;
using System.Text;
using AuthService.Service;
using CatBasicExample.Domain;
using CatBasicExample.Exception;
using CatBasicExample.Repositories;
using CatBasicExample.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CatBasicExample
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
            services.AddControllers()
                    .AddNewtonsoftJson(opt =>
                    {
                        opt.SerializerSettings.ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() };
                        opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            ).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["auth:sk"]))
                };
            });

            services.AddSingleton<Random, Random>()
                    .AddSingleton<IAuthService, AuthService.Service.AuthService>()
                    .AddSingleton<ICatRepository, CatPostgreRepository>()
                    .AddSingleton<ICatService, CatService>()
                    .AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" }))
                    .AddDbContext<CatContext>(initDBContext, ServiceLifetime.Singleton, ServiceLifetime.Singleton);
        }

        private void initDBContext(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder
                .UseNpgsql(Configuration.GetConnectionString("tvc12"))
                .UseSnakeCaseNamingConvention();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(conf =>
                conf.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = string.Empty; //config route swagger
                });
                app.UseMiddleware(typeof(ExceptionHandler));
            }
            if (env.IsProduction() || env.IsStaging())
            {
                app.UseExceptionHandler("/Error"); //user forder 
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
