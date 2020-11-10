using System.IO;
using System.Reflection;

using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

using WebApp;


namespace WebApp.FunctionalTests.DerivedStartup 
{
    /// <summary>
    /// Use WebAppTestStartup, derived from WebApp.Startup with appsettings.json
    /// from this assembly
    /// </summary>
    public class MyWebApplicationFactory : WebApplicationFactory<WebAppTestStartup>
    {
        private string _SettingsFile { get; }


        /// <summary>Initialise _SettingsFile to full path of  appsettings.json file contained in this assembly</summary>
        public MyWebApplicationFactory() {
            string path = Assembly.GetExecutingAssembly().Location;
            
            _SettingsFile = $"{Path.GetDirectoryName(path)}/appsettings.Local.json";
        }

        /// <summary>
        /// Configure web host with config from settings file
        /// </summary>
        /// <seealso cref="Microsoft.AspNetCore.Mvc.Testing.ConfigureWebHost(IWebHostBuilder)"/></seealso>
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseStartup<WebAppTestStartup>()
            .UseEnvironment("Production")
            .ConfigureAppConfiguration((context, cb) =>
            {
                cb.AddJsonFile(_SettingsFile, optional: false)
                .AddEnvironmentVariables();
            })
            // .UseSolutionRelativeContentRoot("Src/WebApp") //works but tests failing in docker container , receive solution root not found
            // .UseTestServer()
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            });


        }

        protected override IHostBuilder CreateHostBuilder()
        {
            var builder = Host.CreateDefaultBuilder()
               .UseServiceProviderFactory(new AutofacServiceProviderFactory());

            return builder;
        }
    }
}

