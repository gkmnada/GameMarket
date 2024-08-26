# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/OrderService/Presentation/Order.API/Order.API.csproj", "src/Services/OrderService/Presentation/Order.API/"]
COPY ["src/Services/OrderService/Infrastructure/Order.Persistence/Order.Persistence.csproj", "src/Services/OrderService/Infrastructure/Order.Persistence/"]
COPY ["src/Services/OrderService/Core/Order.Application/Order.Application.csproj", "src/Services/OrderService/Core/Order.Application/"]
COPY ["src/Contracts/Game.Contracts/Game.Contracts.csproj", "src/Contracts/Game.Contracts/"]
COPY ["src/Services/OrderService/Core/Order.Domain/Order.Domain.csproj", "src/Services/OrderService/Core/Order.Domain/"]
RUN dotnet restore "./src/Services/OrderService/Presentation/Order.API/Order.API.csproj"
COPY . .
WORKDIR "/src/src/Services/OrderService/Presentation/Order.API"
RUN dotnet build "./Order.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Order.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.API.dll"]