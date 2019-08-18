FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /build

COPY ["src/common/infrastructure/ServiceDiscovery/ServiceDiscovery.csproj", "src/common/infrastructure/ServiceDiscovery/"]
RUN dotnet restore "src/common/infrastructure/ServiceDiscovery/ServiceDiscovery.csproj"

COPY ["src/common/infrastructure/ConsulConfiguration/ConsulConfiguration.csproj", "src/common/infrastructure/ConsulConfiguration/"]
RUN dotnet restore "src/common/infrastructure/ConsulConfiguration/ConsulConfiguration.csproj"

COPY ["src/common/infrastructure/Metrics/Metrics.csproj", "src/common/infrastructure/Metrics/"]
RUN dotnet restore "src/common/infrastructure/Metrics/Metrics.csproj"

COPY ["src/services/deliveries/deliveries.csproj", "src/services/deliveries/"]
RUN dotnet restore "src/services/deliveries/deliveries.csproj"
COPY . .
WORKDIR "/build/src/services/deliveries"
RUN dotnet build "deliveries.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "deliveries.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "deliveries.dll"]