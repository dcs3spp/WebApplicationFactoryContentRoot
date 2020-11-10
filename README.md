# Overview

This repository has been created to highlight difficulties encountered while trying to figure out how to use [WebApplicationFactory](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.testing.webapplicationfactory-1?view=aspnetcore-5.0&viewFallbackFrom=aspnetcore-3.1) and the [WebApplicationFactoryContentRootAttribute](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.testing.webapplicationfactorycontentrootattribute?view=aspnetcore-5.0&viewFallbackFrom=aspnetcore-3.1) using the provided documentation.

Please refer to github issues and discussions:

- [Discussion#27643](https://github.com/dotnet/aspnetcore/discussions/27643)
- [Issue#27645](https://github.com/dotnet/aspnetcore/issues/27645)
- [Issue#27627](https://github.com/dotnet/aspnetcore/issues/27627)

This repository highlights the problems that I am encountering while trying to figure out how to use `WebApplicationFactory`.

## Correct usage of WebApplicationFactoryContentRootAttribute

See GitHub [issue](https://github.com/dotnet/aspnetcore/issues/27645) and [discussion](https://github.com/dotnet/aspnetcore/discussions/27643).

When I use [ContentRootPath](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.testing.webapplicationfactorycontentrootattribute.contentrootpath?view=aspnetcore-5.0&viewFallbackFrom=aspnetcore-3.1#Microsoft_AspNetCore_Mvc_Testing_WebApplicationFactoryContentRootAttribute_ContentRootPath) property with an invalid path I cannot get an exception or warning message displayed when running the test assembly. Am I using the attribute correctly? It is as though my usage of the WebApplicationFactoryContentRootAttribute is being ignored, possibly due to incorrect Key property value? Please refer to test project below and try to run `dotnet test` from within that folder:

- Tests/FunctionalTests/WebApp.FunctionalTests

I have also tried to use the `WebApplicationFactoryContentRootAttribute` in
conjunction with a `WebApplicationFactory<TStartup>`, where `TStartup` is derived form the live Startup class, `WebApp.Startup`. The factory uses a specific appsettings.json file. I am trying to set the content root to be the base dir for the live Startup class at Src/WebApp. However, I cannot seem to get the `WebApplicationFactoryContentRootAttribute` attribute to be recognised. Please refer to test project below and try to run `dotnet test` from within that folder:

- Tests/FunctionalTests/WebApp.FunctionalTests.DerivedStartup

## UseSolutionRelativeContentRoot Works In Local Environment But Fails In Docker Container

See GutHub [issue](https://github.com/dotnet/aspnetcore/issues/27627).

I have also tried to use [UseSolutionRelativeContentRoot](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.testhost.webhostbuilderextensions.usesolutionrelativecontentroot?view=aspnetcore-5.0&viewFallbackFrom=aspnetcore-3.1) in
conjunction with a `WebApplicationFactory<TStartup>`, where `TStartup` is derived form a live Startup class, `WebApp.Startup`. Again, the factory uses a specific appsettings.Local.json file.

This time I use the `UseSolutionRelativeContentRoot` method to set the content root to my live WebApp.Startup class at Src/WebApp, relative to the folder containing \*.sln files. This gives some degree of success.

When the test is run using `dotnet test --logger=Console;verbosity=detailed` it runs successfully on a local development environment. However, it is failing when I run it in the Docker container, why?

For details on how I am compiling and running the docker container containing the test, please refer to ../../../Docker/README.md

Please refer to test project below and try to run `dotnet test` from within that folder:

- Tests/FunctionalTests/WebApp.FunctionalTests.UseSolutionRelativeContentRoot
