#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Submission/Submission.API/Submission.API.csproj", "Services/Submission/Submission.API/"]
COPY ["Services/Submission/Submission.Application/Submission.Application.csproj", "Services/Submission/Submission.Application/"]
COPY ["Services/Submission/Submission.Persistence/Submission.Persistence.csproj", "Services/Submission/Submission.Persistence/"]
COPY ["Services/Submission/Submission.Domain/Submission.Domain.csproj", "Services/Submission/Submission.Domain/"]
COPY ["BuildingBlocks/Articles.AspNetCore/Articles.AspNetCore.csproj", "BuildingBlocks/Articles.AspNetCore/"]
COPY ["BuildingBlocks/Articles.AspNetCore/Articles.Entitities.csproj", "BuildingBlocks/Articles.Entitities/"]
COPY ["BuildingBlocks/Articles.AspNetCore/Articles.EntityFrameworkCore.csproj", "BuildingBlocks/Articles.EntityFrameworkCore/"]
COPY ["BuildingBlocks/Articles.AspNetCore/Articles.Exceptions.csproj", "BuildingBlocks/Articles.Exceptions/"]
COPY ["BuildingBlocks/Articles.AspNetCore/Articles.Exceptions.Hanlder.csproj", "BuildingBlocks/Articles.Exceptions.Hanlder/"]
COPY ["BuildingBlocks/Articles.AspNetCore/Articles.MediatR.csproj", "BuildingBlocks/Articles.MediatR/"]
COPY ["BuildingBlocks/Articles.AspNetCore/Articles.Settings.csproj", "BuildingBlocks/Articles.Settings/"]
COPY ["BuildingBlocks/Articles.AspNetCore/Articles.System.csproj", "BuildingBlocks/Articles.System/"]

RUN dotnet restore "./Articles.API/Articles.API.csproj"
COPY . .
WORKDIR "/src/Articles.API"
RUN dotnet build "./Articles.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Articles.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Articles.API.dll"]