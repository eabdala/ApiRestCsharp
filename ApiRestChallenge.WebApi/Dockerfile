#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ApiRestChallenge.WebApi/ApiRestChallenge.WebApi.csproj", "ApiRestChallenge.WebApi/"]
COPY ["ApiRestChallenge.Services/ApiRestChallenge.Services.csproj", "ApiRestChallenge.Services/"]
COPY ["ApiRestChallenge.Abstractions/ApiRestChallenge.Abstractions.csproj", "ApiRestChallenge.Abstractions/"]
COPY ["ApiRestChallenge.Application/ApiRestChallenge.Application.csproj", "ApiRestChallenge.Application/"]
COPY ["ApiRestChallenge.Repository/ApiRestChallenge.Repository.csproj", "ApiRestChallenge.Repository/"]
COPY ["ApiRestChallenge.DataAccess/ApiRestChallenge.DataAccess.csproj", "ApiRestChallenge.DataAccess/"]
COPY ["ApiRestChallenge.Entities/ApiRestChallenge.Entities.csproj", "ApiRestChallenge.Entities/"]
RUN dotnet restore "ApiRestChallenge.WebApi/ApiRestChallenge.WebApi.csproj"
COPY . .
WORKDIR "/src/ApiRestChallenge.WebApi"
RUN dotnet build "ApiRestChallenge.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiRestChallenge.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiRestChallenge.WebApi.dll"]
