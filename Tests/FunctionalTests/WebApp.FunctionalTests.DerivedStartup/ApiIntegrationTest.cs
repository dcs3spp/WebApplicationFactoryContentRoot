using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;


/// Key should match FullName of assembly containing TStartup : WebApplicationFactory<TStartup>
/// This assembly contains class WebAppTestStartup so using key: WebApp.FunctionalTests.DerivedStartup, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
/// https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.testing.webapplicationfactorycontentrootattribute?view=aspnetcore-3.0
/// Also, have set this attribute in the project file for this assembly.
[assembly: WebApplicationFactoryContentRoot(
    "WebApp.FunctionalTests.DerivedStartup, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
    "../../../Src/WebApp",
    "appsettings.json",
    "1")]

namespace WebApp.FunctionalTests.DerivedStartup
{
    public class ApiIntegrationTest : IClassFixture<MyWebApplicationFactory>
    {
        private MyWebApplicationFactory _factory;
        private ITestOutputHelper _output;


        /// <summary>
        /// Initialise derived WebApplicationFactory and output stream
        /// </summary>
        /// <param name="factory"><see cref="MyWebApplicationFactory"/>, derived WebApplicationFactory</param>
        /// <param name="output">xUnit output stream</param>
        public ApiIntegrationTest(MyWebApplicationFactory factory, ITestOutputHelper output)
        {
            _factory = factory;
            _output = output;

            // sanity check for FullName property of assembly containing WebAppTestStartup class
            string fullName = Assembly.GetAssembly(typeof(WebAppTestStartup)).FullName;
            _output.WriteLine($"FullName property of assembly containing 'WebAppTestStartup' class => {fullName}");
        }


        /// <summary>
        /// Try to use the factory to create a client
        /// The factory uses WebAppTestStartup and reads settings from appsettings.Local.json in this assembly
        /// </summary>
        ///
        /// <remarks>
        /// Expecting the WebApplicationContentRoot property to set the contentRoot to ../../../Src/WebApp
        /// Why is WebApplicationFactoryContentRoot being ignored?
        /// What is the correct usage?
        /// </remarks>
        [Fact]
        public async Task WebApp_ApiController_Test()
        {
            const string expectedResponse = "pong";

            var client = _factory.CreateClient();

            var response = await client.GetAsync("http://localhost:5000/api/ping");
            var actualContent = await response.Content.ReadAsStringAsync();

            Assert.Equal(expectedResponse, actualContent);
        }
    }
}
