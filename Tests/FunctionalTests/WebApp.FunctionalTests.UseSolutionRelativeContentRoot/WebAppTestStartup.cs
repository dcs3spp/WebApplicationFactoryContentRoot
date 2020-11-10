using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace WebApp.FunctionalTests.UseSolutionRelativeContentRoot
{
    /// <summary>
    /// Override <see cref="WebApp.Startup"/> to allow customised
    /// configuraion of services and container.
    /// </summary>
    public class WebAppTestStartup : Startup
    {
        public WebAppTestStartup(IConfiguration env) : base(env)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RouteOptions>(Configuration);
            base.ConfigureServices(services);
        }

        public override void ConfigureContainer(ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);
        }
    }
}
