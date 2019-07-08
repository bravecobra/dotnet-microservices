FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ordering/ordering.csproj", "ordering/"]
RUN dotnet restore "ordering/ordering.csproj"
COPY . .
WORKDIR "/src/ordering"
RUN dotnet build "ordering.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ordering.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ordering.dll"]