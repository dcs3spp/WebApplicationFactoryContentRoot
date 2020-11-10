using System.Reflection;
using System.Threading.Tasks;

using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Xunit;
using Xunit.Abstractions;


/// Set contentRoot to an invalid path, expecting Exception or warning to be raised
///
/// Key should match FullName of assembly containing TStartup : WebApplicationFactory<TStartup> 
/// https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.testing.webapplicationfactorycontentrootattribute?view=aspnetcore-3.0
[assembly: WebApplicationFactoryContentRoot(
    "WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
    "/DirectoryDoesNotExist/Src/WebApp",
    "appsettings.json",
    "1")]


namespace WebApp.FunctionalTests
{
    /// <summary>
    /// Expecting exception or warning message to be raised for invalid content root
    /// </summary>
    public class AppTestFixture : WebApplicationFactory<Startup>
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            var builder = base.CreateHostBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory());

            return builder;
        }
    }


    public class ApiIntegrationTest : IClassFixture<AppTestFixture>
    {
        private WebApplicationFactory<Startup> _factory;
        private ITestOutputHelper _output;


        /// <summary>
        /// Initialise derived WebApplicationFactory and output stream
        /// </summary>
        /// <param name="factory"><see cref="AppTestFixture"/>, dervied WebApplicationFactory</param>
        /// <param name="output">xUnit output stream</param>
        public ApiIntegrationTest(AppTestFixture factory, ITestOutputHelper output)
        {
            _factory = factory;
            _output = output;

            // sanity check for FullName property of assembly containing Startup class
            string fullName = Assembly.GetAssembly(typeof(Startup)).FullName;
            _output.WriteLine($"FullName property of assembly containing 'Startup' class => {fullName}");
        }

        /// <summary>
        /// Try to use the factory to create a client
        /// </summary>
        ///
        /// <remarks>
        /// Shouldn't an error be raised here because path is invalid in 
        /// assembly attribute contentRoot property?
        /// 
        /// Why is WebApplicationFactoryContentRoot being ignored?
        /// Is it failing silently and defaulting to dir where *.sln are located?
        /// What is the correct usage?
        /// </remarks>
        [Fact]
        public async Task WebApp_ApiController_Test()
        {
            var exceptionRaised = false;

            try
            {
                var client = _factory.CreateClient();

                await client.GetAsync("http://localhost:5000/api/ping");
            }
            catch
            {
                exceptionRaised = true;
            }

            Assert.True(exceptionRaised);
        }
    }
}
