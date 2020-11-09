using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;

using WebApp;


/// Key should match FullName of assembly containing TStartup : WebApplicationFactory<TStartup> 
/// https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.testing.webapplicationfactorycontentrootattribute?view=aspnetcore-3.0
[assembly: WebApplicationFactoryContentRoot(
    "WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
    "../../Src/WebApp",
    "Program.cs",
    "1")]


namespace WebApp.FunctionalTests
{
    /// <summary>
    /// Eventual Goal: Tryng to create WebApplicationFactory with content root at Src/WebApp using
    /// appsettings file derived from ASPNETCORE_ENVIRONMENT variable. If
    /// the variable is unset then defaults to appsettings.Local.json
    /// </summary>
    public class AppTestFixture : WebApplicationFactory<Startup>
    {
        //override methods here as needed for Test purpose
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
            const string contentRoot = "Src/WebApp";

            _factory = factory;
            _output = output;

            // sanity check for FullName property of assembly containing Startup class
            string fullName = Assembly.GetAssembly(typeof(Startup)).FullName;
            _output.WriteLine($"FullName property of assembly containing 'Startup' class => {fullName}");
        }

        /// <summary>
        /// /Users/me/Development/dotnet/blazormotiondetectionlistener/WebApp/ not found
        ///
        /// Should be path /Users/me/Development/dotnet/blazormotiondetectionlistener/Src/WebApp/
        /// </summary>
        ///
        /// <remarks>
        /// How do I use WebApplicationFactoryContentRoot attribute to correctly resolve this?
        /// </remarks>
        [Fact]
        public async Task WebApp_App_ApiController_Test()
        {
            var client = _factory.CreateClient();

            await Task.CompletedTask;
        }
    }
}
