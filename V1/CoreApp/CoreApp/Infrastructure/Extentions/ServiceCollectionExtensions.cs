using Microsoft.Extensions.DependencyInjection;
using System;
using Autofac;
using CoreApp.Configurations.DependencyInjections;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CoreApp.Common.Constants;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using CoreApp.Authentication.Authorizations;
using CoreApp.Authentication.Models;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using Microsoft.OpenApi.Models;

namespace CoreApp.Infrastructure.Extentions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="applicationContainer"></param>
        /// <returns></returns>
        public static IServiceProvider ConfigureApplicationServices(this IServiceCollection services, Autofac.IContainer applicationContainer, IConfiguration configuration, SymmetricSecurityKey signingKey)
        {

            // I. DB Context
            var connection = configuration.GetConnectionString(SqlConstant.DatabaseConnectionName.MainDatabaseConnectionName);
            services.AddDbContext<EntityFramework.Models.CoreAppDbContext>(options => options.UseSqlServer(connection));

            // II. Common
            services.AddSignalR();
            services.AddAutoMapper();
            // To enable Cors
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            // Add session for captcha
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(5);
                options.Cookie.HttpOnly = true;
            });

            // III. Authentication 
            // 1. Authen by Cookie
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            //{
            //    options.ExpireTimeSpan = TimeSpan.FromDays(7);
            //    options.Events.OnRedirectToLogin = context =>
            //    {
            //        context.Response.StatusCode = 401;
            //        return Task.CompletedTask;
            //    };
            //});

            // Build in Dependency Injection
            services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, ModuleBasedAuthorizationHandler>();
            services.AddSingleton<Authentication.Jwt.IJwtFactory, Authentication.Jwt.JwtFactory>();
            services.TryAddTransient<Authentication.Jwt.IUserManager, Authentication.Jwt.UserManager>();


            // 2. Authen by JWT
            services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();

            // Get app setting
            var jwtAppSettingOptions = configuration.GetSection(nameof(JwtIssuerOptions));
            var facebookAuthSettings = configuration.GetSection(nameof(FacebookAuthSettings));

            // Configure setting to app
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });
            services.Configure<FacebookAuthSettings>(options =>
            {
                options.AppId = facebookAuthSettings[nameof(FacebookAuthSettings.AppId)];
                options.AppSecret = facebookAuthSettings[nameof(FacebookAuthSettings.AppSecret)];
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication()
            .AddJwtBearer("Custom", configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            })
            .AddJwtBearer("Firebase", options =>
            {
                options.Authority = "https://securetoken.google.com/ninoapp-c6fbb";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "https://securetoken.google.com/ninoapp-c6fbb",
                    ValidateAudience = true,
                    ValidAudience = "ninoapp-c6fbb",
                    ValidateLifetime = true
                };
            });

            services
                .AddAuthorization(options =>
                {
                    options.DefaultPolicy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .AddAuthenticationSchemes("Firebase", "Custom")
                        .Build();
                });
            // IV. Mvc
            services.AddRouting(opts => opts.LowercaseUrls = true);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //services.AddSpaStaticFiles(config => { config.RootPath = "ClientApp/dist"; });

            // VI. Swagger
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Api Helper", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey",
                    Description = "Authorization Key: Z29vZEtleQ=="
                });
            });

            // Config JSON result.
            // Default ContractResolver is CamelCase
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling =
                                           ReferenceLoopHandling.Ignore;
                //options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            // V. Web Api
            var apiControllerAssembly = Assembly.Load(new AssemblyName("CoreApp.WebApi"));
            services.AddMvc().AddApplicationPart(apiControllerAssembly).AddControllersAsServices();


            // VII. Autofac Dependency Injection
            var builder = new ContainerBuilder();
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<DomainModule>();
            builder.RegisterModule<ApplicationModule>();
            builder.Populate(services);
            applicationContainer = builder.Build();
            return new AutofacServiceProvider(applicationContainer);
        }
    }
}
