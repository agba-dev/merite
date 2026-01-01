# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/projects/NuGet.Config", "services/projects/"]
COPY ["services/administration/NuGet.Config", "services/administration/"]
COPY ["services/projects/host/Merite.Projects.HttpApi.Host/Merite.Projects.HttpApi.Host.csproj", "services/projects/host/Merite.Projects.HttpApi.Host/"]
COPY ["shared/Merite.Microservice.Shared/Merite.Microservice.Shared.csproj", "shared/Merite.Microservice.Shared/"]
COPY ["services/administration/src/Merite.Administration.EntityFrameworkCore/Merite.Administration.EntityFrameworkCore.csproj", "services/administration/src/Merite.Administration.EntityFrameworkCore/"]
COPY ["shared/Merite.Shared/Merite.Shared.csproj", "shared/Merite.Shared/"]
COPY ["services/administration/src/Merite.Administration.Domain/Merite.Administration.Domain.csproj", "services/administration/src/Merite.Administration.Domain/"]
COPY ["services/administration/src/Merite.Administration.Domain.Shared/Merite.Administration.Domain.Shared.csproj", "services/administration/src/Merite.Administration.Domain.Shared/"]
COPY ["shared/Merite.Hosting.Shared/Merite.Hosting.Shared.csproj", "shared/Merite.Hosting.Shared/"]
COPY ["shared/Merite.ServiceDefaults/Merite.ServiceDefaults.csproj", "shared/Merite.ServiceDefaults/"]
COPY ["services/projects/src/Merite.Projects.HttpApi/Merite.Projects.HttpApi.csproj", "services/projects/src/Merite.Projects.HttpApi/"]
COPY ["services/projects/src/Merite.Projects.Application.Contracts/Merite.Projects.Application.Contracts.csproj", "services/projects/src/Merite.Projects.Application.Contracts/"]
COPY ["services/projects/src/Merite.Projects.Domain.Shared/Merite.Projects.Domain.Shared.csproj", "services/projects/src/Merite.Projects.Domain.Shared/"]
COPY ["services/projects/src/Merite.Projects.Application/Merite.Projects.Application.csproj", "services/projects/src/Merite.Projects.Application/"]
COPY ["services/projects/src/Merite.Projects.Domain/Merite.Projects.Domain.csproj", "services/projects/src/Merite.Projects.Domain/"]
COPY ["services/projects/src/Merite.Projects.EntityFrameworkCore/Merite.Projects.EntityFrameworkCore.csproj", "services/projects/src/Merite.Projects.EntityFrameworkCore/"]
RUN dotnet restore "./services/projects/host/Merite.Projects.HttpApi.Host/Merite.Projects.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/services/projects/host/Merite.Projects.HttpApi.Host"
RUN dotnet build "./Merite.Projects.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Merite.Projects.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Merite.Projects.HttpApi.Host.dll"]