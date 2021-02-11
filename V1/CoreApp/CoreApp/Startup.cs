using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Autofac;
using Microsoft.Extensions.Logging;
using CoreApp.Infrastructure.Extentions;
using CoreApp.Logger.Extentions;
using CoreApp.Common.Constants;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace CoreApp
{
    public class Startup
    {
        #region Fields
        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; }

        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH";
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        #endregion

        #region Contructors
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        #region Methods
        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAnyOrigin",
            //        builder => builder
            //        .AllowAnyOrigin()
            //        .AllowAnyMethod()
            //        .AllowAnyHeader())
            //    ;
            //});


            return services.ConfigureApplicationServices(ApplicationContainer, Configuration, _signingKey);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseStaticFiles();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddContext(LogLevel.Error, Configuration.GetConnectionString(SqlConstant.DatabaseConnectionName.LoggerDatabaseConnectionName));

            app.UseExceptionHandler(env);

            //app.UseCoreAppStaticFiles(enableCache: true);

            // Note: The position is very important. it must always above UseMvc.
            app.UseAuthentication();
            //app.UseSignalR((options) =>
            //{
            //    options.MapHub<ChangeProxyHub>("/hubs/changeproxy");
            //});

            app.UseCors(options => options.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            

            // Swagger ui
            app.UseCoreAppSwagger();

            // Mvc
            //app.UseHttpsRedirection();
            app.UseSession();
            app.UseCoreAppMvc();
            app.UseCoreAppDirectoryBrowser();


            // Single page.
            //app.UseCoreAppSPA(env);
        }
        #endregion
    }
}
