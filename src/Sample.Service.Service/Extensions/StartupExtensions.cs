using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sample.Service.Business.Implementation;
using Sample.Service.Business.Interfaces;
using Sample.Service.DataAccess.Implementation.Repositories;
using Sample.Service.DataAccess.Interfaces.Repositories;
using Sample.Service.Service.Filters;

namespace Sample.Service.Service.Extensions
{
    /// <summary>
    /// Extensions to configure the Startup.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class StartupExtensions
    {
        /// <summary>
        /// Configure services to use in the microservice.
        /// </summary>
        /// <param name="services">Services.</param>
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IGenreRepository, GenreRepository>();

            services.AddTransient<IBookBusiness, BookBusiness>();
            services.AddTransient<IAuthorBusiness, AuthorBusiness>();
            services.AddTransient<IGenreBusiness, GenreBusiness>();

            services.AddScoped<ValidateModelAttribute>();
            services.AddTransient<HttpClient, HttpClient>();
        }

        /// <summary>
        /// Adds the cors, Allow any origin.
        /// </summary>
        /// <param name="services">Services.</param>
        public static void AddCORS(this IServiceCollection services)
        {
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOriginsHeadersAndMethods",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });
        }

        /// <summary>
        /// Adds the healthchecks of all the services.
        /// </summary>
        /// <param name="services">Services.</param>
        /// <param name="connection">Connection.</param>
        /// <param name="servicesUrls">Services urls.</param>
        public static void AddCustomHealthChecks(this IServiceCollection services, string? connection, Dictionary<string, string?>? servicesUrls)
        {
            if (!string.IsNullOrEmpty(connection))
            {
                services.AddHealthChecks()
                    .AddNpgSql(connection, tags: new[] { "ready" });
            }

            if (servicesUrls != null)
            {
                foreach (var item in servicesUrls)
                {
                    if (!string.IsNullOrEmpty(item.Value))
                    {
                        services.AddHealthChecks()
                            .AddUrlGroup(new Uri($"{item.Value}/ping"), item.Key, HealthStatus.Degraded, tags: new[] { "ready" });
                    }
                }
            }
        }

        /// <summary>
        /// Add open api.
        /// </summary>
        /// <param name="services">Services.</param>
        /// <param name="currentEnvironment">Current environment.</param>
        public static void AddOpenApi(this IServiceCollection services, IHostEnvironment currentEnvironment)
        {
            if (currentEnvironment.IsDevelopment())
            {
                services.AddOpenApiDocument(config =>
                {
                    config.PostProcess = document =>
                    {
                        document.Info.Version = "v1";
                        document.Info.Title = "Conekta Sample Service";
                        document.Info.Contact = new NSwag.OpenApiContact
                        {
                            Name = "conekta@conekta.com",
                            Email = "conekta@conekta.com"
                        };
                    };
                });
            }
        }

        /// <summary>
        /// Add logging.
        /// </summary>
        /// <param name="services">Services.</param>
        /// <param name="configuration">Configuration.</param>
        /// <param name="currentEnvironment">Current environment.</param>
        public static void AddLogging(this IServiceCollection services, IConfiguration configuration,
            IHostEnvironment currentEnvironment)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));
                if (currentEnvironment.IsEnvironment("Testing") || currentEnvironment.IsDevelopment())
                {
                    loggingBuilder.AddConsole();
                    loggingBuilder.AddDebug();
                }
            });
        }
    }
}
