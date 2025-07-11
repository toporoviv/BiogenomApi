FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BiogenomApi.Api/BiogenomApi.Api.csproj", "BiogenomApi.Api/"]
COPY ["BiogenomApi.Infrastructure/BiogenomApi.Infrastructure.csproj", "BiogenomApi.Infrastructure/"]
COPY ["BiogenomApi.Domain/BiogenomApi.Domain.csproj", "BiogenomApi.Domain/"]
COPY ["BiogenomApi.Services/BiogenomApi.Services.csproj", "BiogenomApi.Services/"]
RUN dotnet restore "BiogenomApi.Api/BiogenomApi.Api.csproj"
COPY . .
WORKDIR "/src/BiogenomApi.Api"
RUN dotnet build "BiogenomApi.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BiogenomApi.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BiogenomApi.Api.dll"]
