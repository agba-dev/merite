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
COPY ["services/administration/NuGet.Config", "services/administration/"]
COPY ["services/identity/NuGet.Config", "services/identity/"]
COPY ["services/saas/NuGet.Config", "services/saas/"]
COPY ["apps/Merite.AuthServer/Merite.AuthServer.csproj", "apps/Merite.AuthServer/"]
COPY ["services/administration/src/Merite.Administration.EntityFrameworkCore/Merite.Administration.EntityFrameworkCore.csproj", "services/administration/src/Merite.Administration.EntityFrameworkCore/"]
COPY ["shared/Merite.Shared/Merite.Shared.csproj", "shared/Merite.Shared/"]
COPY ["services/administration/src/Merite.Administration.Domain/Merite.Administration.Domain.csproj", "services/administration/src/Merite.Administration.Domain/"]
COPY ["services/administration/src/Merite.Administration.Domain.Shared/Merite.Administration.Domain.Shared.csproj", "services/administration/src/Merite.Administration.Domain.Shared/"]
COPY ["services/identity/src/Merite.IdentityService.EntityFrameworkCore/Merite.IdentityService.EntityFrameworkCore.csproj", "services/identity/src/Merite.IdentityService.EntityFrameworkCore/"]
COPY ["services/identity/src/Merite.IdentityService.Domain/Merite.IdentityService.Domain.csproj", "services/identity/src/Merite.IdentityService.Domain/"]
COPY ["services/identity/src/Merite.IdentityService.Domain.Shared/Merite.IdentityService.Domain.Shared.csproj", "services/identity/src/Merite.IdentityService.Domain.Shared/"]
COPY ["services/saas/src/Merite.SaaS.EntityFrameworkCore/Merite.SaaS.EntityFrameworkCore.csproj", "services/saas/src/Merite.SaaS.EntityFrameworkCore/"]
COPY ["services/saas/src/Merite.SaaS.Domain/Merite.SaaS.Domain.csproj", "services/saas/src/Merite.SaaS.Domain/"]
COPY ["services/saas/src/Merite.SaaS.Domain.Shared/Merite.SaaS.Domain.Shared.csproj", "services/saas/src/Merite.SaaS.Domain.Shared/"]
COPY ["shared/Merite.Microservice.Shared/Merite.Microservice.Shared.csproj", "shared/Merite.Microservice.Shared/"]
COPY ["shared/Merite.Hosting.Shared/Merite.Hosting.Shared.csproj", "shared/Merite.Hosting.Shared/"]
COPY ["shared/Merite.ServiceDefaults/Merite.ServiceDefaults.csproj", "shared/Merite.ServiceDefaults/"]
RUN dotnet restore "./apps/Merite.AuthServer/Merite.AuthServer.csproj"
COPY . .
WORKDIR "/src/apps/Merite.AuthServer"
RUN dotnet build "./Merite.AuthServer.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Merite.AuthServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Merite.AuthServer.dll"]