#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["netcoreTemplate.api/netcoreTemplate.Api.csproj", "netcoreTemplate.api/"]
COPY ["netcoreTemplate.application/netcoreTemplate.Application.csproj", "netcoreTemplate.application/"]
COPY ["netcoreTemplate.Domain/netcoreTemplate.Domain.csproj", "netcoreTemplate.Domain/"]
COPY ["netcoreTemplate.Infrastructure/netcoreTemplate.Infrastructure.csproj", "netcoreTemplate.Infrastructure/"]
RUN dotnet restore "netcoreTemplate.api/netcoreTemplate.Api.csproj"
COPY . .
WORKDIR "/src/netcoreTemplate.api"
RUN dotnet build "netcoreTemplate.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "netcoreTemplate.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "netcoreTemplate.Api.dll"]