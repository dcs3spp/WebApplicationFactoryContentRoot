# Overview

Integration test foor WebApp controller, http://localhost:5000/api/ping endpoint.

This uses WebApplicationFactory<TStartup> where TStartup class is:

1. A class in the test assembly, WebAppTestStartup, derived from the live startup class `WebApp.Startup`
2. MyWebApplicationFactory is the custom WebApplicationFactory used in this assembly.

Class MyWebApplicationFactory uses `UseStartup` on `IWebHostBuilder` in the overidden ConfigureWebHost
method. It also loads the `appsettings.Local.json` file contained in this test assembly.

This projects _csproj_ file contains WebApplicationContentRootAttribute with:

1. Key: Set to full assembly name of WebAppTestStartup
2. ContentRootPath: ../../../Src/WebApp
3. ContentRootTest: appsettings.json
4. Priority: 1

This assembly attribute is also set in ApiIntegrationTest.cs in this assembly.

When the test is run using `dotnet test --logger=Console;verbosity=detailed` an exception is thrown
System.IO.DirectoryNotFoundException : 'solutionDir'/WebApp.FunctionalTests.DerivedStartup/

With the content root attribute set should this not be 'solutionDir'/Src/WebApp.FunctionalTests.DerivedStartup?

```bash
 Error Message:
   System.IO.DirectoryNotFoundException : /Users/simon/Development/dotnet/scratch/webapplicationcontentroot/WebApp.FunctionalTests.DerivedStartup/
  Stack Trace:
     at Microsoft.Extensions.FileProviders.PhysicalFileProvider..ctor(String root, ExclusionFilters filters)
   at Microsoft.Extensions.FileProviders.PhysicalFileProvider..ctor(String root)
   at Microsoft.Extensions.Hosting.HostBuilder.CreateHostingEnvironment()
   at Microsoft.Extensions.Hosting.HostBuilder.Build()
   at Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory`1.CreateHost(IHostBuilder builder)
   at Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory`1.EnsureServer()
   at Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory`1.CreateDefaultClient(DelegatingHandler[] handlers)
   at Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory`1.CreateDefaultClient(Uri baseAddress, DelegatingHandler[] handlers)
   at Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory`1.CreateClient(WebApplicationFactoryClientOptions options)
   at Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory`1.CreateClient()
   at WebApp.FunctionalTests.DerivedStartup.ApiIntegrationTest.WebApp_App_ApiController_Test() in /Users/simon/Development/dotnet/scratch/webapplicationcontentroot/Tests/FunctionalTests/WebApp.FunctionalTests.DerivedStartup/ApiIntegrationTest.cs:line 70
--- End of stack trace from previous location where exception was thrown ---
```
