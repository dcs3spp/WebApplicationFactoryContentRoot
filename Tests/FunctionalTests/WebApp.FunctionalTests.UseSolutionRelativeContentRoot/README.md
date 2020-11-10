# Overview

Integration test for WebApp controller, http://localhost:5000/api/ping endpoint.

This uses WebApplicationFactory<TStartup> where TStartup class is:

1. A class in the test assembly, WebAppTestStartup, derived from the live startup class `WebApp.Startup`
2. MyWebApplicationFactory is the custom WebApplicationFactory used in this assembly.

Class MyWebApplicationFactory uses `UseStartup` on `IWebHostBuilder` in the overidden ConfigureWebHost
method. It also loads the `appsettings.Local.json` file contained in this test assembly.

This test assembly does not use `WebApplicationFactoryContentRootAttribute`. However, it does
use `UseSolutionRelativeContentRoot` to set the content root relative to the folder
containing \*.sln files.

When the test is run using `dotnet test --logger=Console;verbosity=detailed` it runs successfully on
local development environment. However, it fails in Docker container, why?

For details on how to compile and run the docker container containing the test, please use the README.md
at ../../../Docker/README.md
