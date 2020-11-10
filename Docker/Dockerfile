##
# Dockerfile for testing Tests/FunctionalTests/WebApp.FunctionalTests.UseSolutionRelativeContentRoot
# Intended for use in conjunction with docker buildkit
#
# Small test container to demonstrate failing UseSolutionRelativeContentRoot issue in docker
# container
#
# See Github:
# - [Issue#27627](https://github.com/dotnet/aspnetcore/issues/27627)
#
#
# Stages:
# base:             Contains alpine packages and dotnet tools
# build-test:       Development dependencies restored
# test:             Enables entrypoint for testing
#
# Usage:
# Build:            DOCKER_BUILDKIT=1 docker build -f Docker/Dockerfile -t docker-webapp .
# Run:              docker run --rm docker-webapp
#
# To start shell in container:
#                   docker run --rm --entrypoint=bash -it docker-webapp
##


##
# Base Image
# Image: SDK
# 
# - Installs reportgenerator global tool and updates path
# - Sets PS1 prompt coloured to include user@host and working directory
##
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS base

ENV PS1A="[\d \[\e[33m\]\t\[\e[m\]] \[\e[31m\]\u\[\e[m\]@\[\e[31m\]\h\[\e[m\] \[\e[36m\]\w\[\e[m\]\n $ "

RUN apk add --update --no-cache git bash curl zip \
    && echo 'PS1=$PS1A' >> ~/.bashrc


##
# Build Dependencies for testing
# Mirror the structure of local development environment
##
FROM base AS build-test

COPY "WebApp.Docker.sln" "WebApp.Docker.sln"

WORKDIR /Src
COPY "Src/WebApp/WebApp.csproj" "WebApp/WebApp.csproj"

WORKDIR /Tests
COPY "Tests/FunctionalTests/WebApp.FunctionalTests.UseSolutionRelativeContentRoot/WebApp.FunctionalTests.UseSolutionRelativeContentRoot.csproj" "FunctionalTests/WebApp.FunctionalTests.UseSolutionRelativeContentRoot/WebApp.FunctionalTests.UseSolutionRelativeContentRoot.csproj"

WORKDIR /
RUN dotnet restore -r linux-musl-x64 --no-cache "WebApp.Docker.sln"

COPY ./Src /Src/
COPY ./Tests /Tests/


##
# Entrypoint for testing
##
FROM build-test AS test
ENTRYPOINT ["dotnet", "test", "--logger:trx", "WebApp.Docker.sln"]
