# Overview

Integration test for WebApp controller, http://localhost:5000/api/ping endpoint.

This uses WebApplicationFactory<TStartup> where TStartup class is `WebApp.Startup`, i.e.
the live Startup class.

This project's _csproj_ file contains WebApplicationContentRootAttribute with:

1. Key: Set to full assembly name of WebApp
2. ContentRootPath: /DirectoryDoesNotExist/Src/WebApp
3. ContentRootTest: appsettings.json
4. Priority: 1

This assembly attribute is also set in ApiIntegrationTest.cs.

When the test is run using `dotnet test --logger=Console;verbosity=detailed` no exception or warning message
is displayed due to an invalid path in ContentRootPath property of WebApplicationContentRootAttribute.

It looks as though it is silently failing and falling back to the directory containing the solution files.
