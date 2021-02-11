using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Linq;

namespace CoreApp.Infrastructure.Extentions
{
    public static class ApplicationBuilderExtentions
    {
        /// <summary>
        /// Add excetion handling
        /// </summary>
        /// <param name="app">Builder for configuring an application's request pipeline</param>
        /// <param name="env">Hosting environment</param>
        public static void UseExceptionHandler(this IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
        }

        /// <summary>
        /// Add Mvc, Api map routes configuring 
        /// </summary>
        /// <param name="app">Builder for configuring an application's request pipeline</param>
        public static void UseCoreAppMvc(this IApplicationBuilder app)
        {
            // MVC
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //app.MapWhen(context => !context.Request.Path.StartsWithSegments("/admin") &&
            //!context.Request.Path.Value.Contains(".") &&
            //!context.Request.Path.StartsWithSegments("/sockjs-node")
            //, (appBuilder) =>
            //{
            //    appBuilder.UseMvc(routes =>
            //    {
            //        //For frontend reacjs
            //        routes.MapSpaFallbackRoute(
            //              name: "reactjs-fallback",
            //              defaults: new { controller = "Home", action = "Index" });
            //    });
            //});
        }

        /// <summary>
        /// Configure Static files
        /// </summary>
        /// <param name="app">Builder for configuring an application's request pipeline</param>
        /// <param name="enableCache">Enable cache header</param>
        public static void UseCoreAppStaticFiles(this IApplicationBuilder app, bool enableCache)
        {
            if (enableCache)
            {
                app.UseStaticFiles(new StaticFileOptions
                {
                    OnPrepareResponse = ctx =>
                    {
                        // max-age=86400 => 1 day
                        ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=86400");
                    }
                });
            }
            else
            {
                app.UseStaticFiles(new StaticFileOptions
                {
                    OnPrepareResponse = ctx =>
                    {
                        ctx.Context.Response.Headers.Append("Cache-Control", "no-cache, no-store, must-revalidate");
                    }
                });
            }
        }

        /// <summary>
        /// Configure enable access directory on browser
        /// </summary>
        /// <param name="app">Builder for configuring an application's request pipeline</param>
        public static void UseCoreAppDirectoryBrowser(this IApplicationBuilder app)
        {
            // Set up custom content types - associating file extension to MIME type
            var provider = new FileExtensionContentTypeProvider();
            // Config mappings
            provider.Mappings[".xlsx"] = "application/x-msdownload";

            app.UseStaticFiles();
            //app.UseSpaStaticFiles();

            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images")),
                RequestPath = "/MyFiles"
            });
        }

        /// <summary>
        /// Configure default files to run when app start
        /// </summary>
        /// <param name="app">Builder for configuring an application's request pipeline</param>
        public static void UseCoreAppDefaultFiles(this IApplicationBuilder app)
        {
            // Serve my app-specific default file, if present.
            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("mydefault.html");
            app.UseDefaultFiles(options);
        }

        /// <summary>
        /// Configure single page application
        /// </summary>
        /// <param name="app"></param>
        public static void UseCoreAppSPA(this IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }

        /// <summary>
        /// User swagger ui
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public static void UseCoreAppSwagger(this IApplicationBuilder app)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Helper");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
