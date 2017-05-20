using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.Swagger;

namespace SwagApiMvcTest
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddMvcCore().AddVersionedApiExplorer();
            services.AddApiVersioning(opts => {
                opts.ReportApiVersions = true;
            });
            services.AddSwaggerGen(opts => {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in provider.ApiVersionDescriptions) {
                    opts.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
                }
                // Implicitly sets the API-Version in the SwaggerUI Test Method
                opts.OperationFilter<ImplicitApiVersionParameter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApiVersionDescriptionProvider provider)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc().UseApiVersioning();
            app.UseSwagger();
            app.UseSwaggerUI(opts => {
                foreach (var description in provider.ApiVersionDescriptions) {
                    opts.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });
        }

        static Info CreateInfoForApiVersion(ApiVersionDescription description) {
            var info = new Info() {
                Title = $"LeTFS API {description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
                Description = "LeTFS API Documentation"
            };

            if (description.IsDeprecated) {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}
