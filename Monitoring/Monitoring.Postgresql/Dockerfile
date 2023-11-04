FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["Monitoring/Monitoring.Postgresql/Monitoring.Postgresql.csproj", "Monitoring/Monitoring.Postgresql/"]
RUN dotnet restore "Monitoring/Monitoring.Postgresql/Monitoring.Postgresql.csproj"
COPY . .
WORKDIR "/src/Monitoring/Monitoring.Postgresql"
RUN dotnet build "Monitoring.Postgresql.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Monitoring.Postgresql.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Monitoring.Postgresql.dll"]
