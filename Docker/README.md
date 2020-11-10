# Overview

Create a docker container environment for tests in project `Tests/WebApp.FunctionalTests.UseSolutionRelativeContentRoot`

This test project uses a TestStartup class with custom a custom appsettings.Local.json file.
It uses `UseSolutionRelativeContentRoot` to set the content root path relative to folder container \*.sln files.

The test passes in local development environment but is failing when run in docker environment. Can anyone help?

## See Github:

- [Issue#27627](https://github.com/dotnet/aspnetcore/issues/27627)

## Usage

```C#
# from root dir
 DOCKER_BUILDKIT=1 docker build -f Docker/Dockerfile -t docker-webapp .

# run tests in the container
docker run --rm docker-webapp

# explore further by starting a bash shell in container
 docker run --rm --entrypoint=bash -it docker-webapp
```
