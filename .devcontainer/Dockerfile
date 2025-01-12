FROM mcr.microsoft.com/devcontainers/dotnet:1-9.0-bookworm

RUN dotnet tool install csharpier -g
RUN dotnet tool install --global Microsoft.OpenApi.Kiota

RUN dotnet dev-certs https --trust
