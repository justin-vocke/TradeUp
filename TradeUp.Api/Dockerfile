#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TradeUp.Api/TradeUp.Api.csproj", "TradeUp.Api/"]
COPY ["TradeUp.Infrastructure.IoC/TradeUp.Infrastructure.IoC.csproj", "TradeUp.Infrastructure.IoC/"]
COPY ["TradeUp.Application/TradeUp.Application.csproj", "TradeUp.Application/"]
COPY ["TradeUp.Domain.Core/TradeUp.Domain.Core.csproj", "TradeUp.Domain.Core/"]
COPY ["TradeUp.BackgroundServices/TradeUp.BackgroundServices.csproj", "TradeUp.BackgroundServices/"]
COPY ["TradeUp.Infrastructure.Bus/TradeUp.Infrastructure.Bus.csproj", "TradeUp.Infrastructure.Bus/"]
COPY ["TradeUp.Infrastructure/TradeUp.Infrastructure.csproj", "TradeUp.Infrastructure/"]
RUN dotnet restore "./TradeUp.Api/TradeUp.Api.csproj"
COPY . .
WORKDIR "/src/TradeUp.Api"
RUN dotnet build "./TradeUp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TradeUp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TradeUp.Api.dll"]