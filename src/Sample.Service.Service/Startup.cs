using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Sample.Service.DataAccess.Dto.Common;
using Sample.Service.Models;
using Sample.Service.Service.Exceptions;
using Sample.Service.Service.Extensions;
using Sample.Service.Service.Models;

namespace Sample.Service.Service
{
    /// <summary>
    /// Startup.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        #region :: Properties ::

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// The current environment.
        /// </summary>
        private readonly IHostEnvironment CurrentEnvironment;

        #endregion

        #region :: Constructor ::

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sample.Service.Service.Startup"/> class.
        /// </summary>
        /// <param name="configuration">Configuration.</param>
        /// <param name="env">Environment variables</param>
        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
            CurrentEnvironment = env;
        }

        #endregion

        #region :: Methods ::

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            string? connection = string.Empty;
            Dictionary<string, string>? secrets = null;

            if (CurrentEnvironment.IsEnvironment("Testing") || CurrentEnvironment.IsDevelopment())
            {
                connection = Environment.GetEnvironmentVariable("CONNECTION");
            }
            else if (CurrentEnvironment.IsStaging() || CurrentEnvironment.IsProduction())
            {
                secrets = JsonConvert.DeserializeObject<Dictionary<string, string>>(SecretsManager.GetSecret());
                connection = StartupUtils.GetSecretsValue("CONNECTION", secrets);
            }

            services.AddEntityFrameworkNpgsql().AddDbContext<SampleDbContext>(
                (sp, opt) => opt.UseNpgsql(connection,
                target => target.MigrationsAssembly("Sample.Service.Models"))
                .UseInternalServiceProvider(sp));

            services.AddControllers()
                .AddNewtonsoftJson(
                    options => options.SerializerSettings.ReferenceLoopHandling =
                        ReferenceLoopHandling.Ignore
                );

            services.AddApiVersioning(
                options =>
                {
                    // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                    options.ReportApiVersions = true;

                    // automatically applies an api version based on the name of the defining controller's namespace
                    options.Conventions.Add(new VersionByNamespaceConvention());
                });

            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.SuppressModelStateInvalidFilter = true;
            });

            services.AddCustomHealthChecks(connection, null);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddCORS();
            services.AddOpenApi(CurrentEnvironment);
            services.AddLogging(Configuration, CurrentEnvironment);

            services.Configure<CustomErrors>(options => Configuration.GetSection("CustomErrors").Bind(options));
            var config = Configuration.GetSection("CustomMessages").Get<CustomMessages>();
            services.AddSingleton(config);
            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.SuppressModelStateInvalidFilter = true;
            });

            services.ConfigureServices();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<SampleDbContext>();

                if (context.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                {
                    context.Database.Migrate();
                }
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseOpenApi();
                app.UseSwaggerUi3();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRouting();
            app.UseCors("AllowOriginsHeadersAndMethods");
            app.UseEndpoints(endpoint =>
            {
                endpoint.MapControllers();
            });
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseHttpsRedirection();

            var options = new HealthCheckOptions();
            options.ResultStatusCodes[HealthStatus.Unhealthy] = StatusCodes.Status500InternalServerError;
            options.ResponseWriter = StartupUtils.HealthResponseWriter;
            app.UseHealthChecks("/health", options);

            var pingOptions = new HealthCheckOptions();
            pingOptions.ResultStatusCodes[HealthStatus.Unhealthy] = StatusCodes.Status500InternalServerError;
            pingOptions.ResponseWriter = StartupUtils.HealthResponseWriter;
            pingOptions.Predicate = (check) => false;
            app.UseHealthChecks("/ping", pingOptions);
        }

        #endregion
    }
}
