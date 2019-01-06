using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KenDotNetWithAng6
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            /*
             Add middleware to support SPA in .NET Core App. And to do that, 
             open Startup.cs and register the SPA service that
             can provide static files to be served for a Single Page Application (SPA). 
             */
            /*
             c.RootPath = "KenApp/dist" defines where your all static files will be dumped.
             Now the question arises: why do we need this path if we already have wwwroot
             which is meant to hold static files in .NET Core. 
             ********This topic will be captured once we come to our final build and deployment step later in the series********
             */
            services.AddSpaStaticFiles(c =>
            {
                c.RootPath = "KenApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            /* to support SPA files, .NET Core provides middleware and we have to add these in configure method*/
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            /*Default import*/
            app.UseHttpsRedirection();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            /* to support SPA files, .NET Core provides middleware and we have to add these in configure method*/
            app.UseSpa(spa =>
            {
                //This will ensure that all the static contents are going to load from ClientApp folder.
                spa.Options.SourcePath = "KenApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
