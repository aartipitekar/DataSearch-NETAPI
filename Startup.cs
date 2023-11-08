using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using DataSearch.Models;
using DataSearch.Services;
using Newtonsoft.Json;

namespace DataSearch
{
    /// <summary>
    /// Configuration and setup for .NET api application.
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private List<Users> _dummyData;

        /// <summary>
        /// Initializes a new instance of the Startup class.
        /// </summary>
        /// <param name="configuration">Configuration settings for the application.</param>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _dummyData = new List<Users>();
            ConfigureDummyData(_dummyData);
        }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        /// <param name="services">The collection of services to configure.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Add CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp",
                    builder => builder.WithOrigins("http://localhost:3000")
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });

            // Initialize and populate the dummy data
            var dummyData = new List<Users>();
            ConfigureDummyData(dummyData);

            // Register the SearchService with the dummy data
            services.AddSingleton<SearchService>(provider =>
                new SearchService(dummyData, provider.GetRequiredService<ILogger<SearchService>>()));
        }

        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="env">The hosting environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowReactApp");
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// Configures the dummy data for the application.
        /// </summary>
        /// <param name="users">The list to populate with dummy data from dummydata.json.</param>
        public void ConfigureDummyData(List<Users> users)
        {
            var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "dummydata.json");

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);

                try
                {
                    var dummyData = JsonConvert.DeserializeObject<List<Users>>(jsonContent);

                    if (dummyData != null)
                    {
                        users.AddRange(dummyData);
                    }
                    else
                    {
                        Console.WriteLine("Warning: Unable to deserialize JSON content");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                    
                }
            }
            else
            {
                Console.WriteLine("Warning: JSON file not found");
            }
        }
    }
}
