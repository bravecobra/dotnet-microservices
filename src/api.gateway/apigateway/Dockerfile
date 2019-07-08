FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["api.gateway/apigateway/apigateway.csproj", "api.gateway/apigateway/"]
RUN dotnet restore "api.gateway/apigateway/apigateway.csproj"
COPY . .
WORKDIR "/src/api.gateway/apigateway"
RUN dotnet build "apigateway.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "apigateway.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "apigateway.dll"]