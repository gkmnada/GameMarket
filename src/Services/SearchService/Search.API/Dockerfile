
# First Stage: Build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Expose ports
EXPOSE 8080
EXPOSE 8081

# Copy solution and project files
COPY GameMarket.sln GameMarket.sln
COPY src/Contracts/Game.Contracts/Game.Contracts.csproj src/Contracts/Game.Contracts/Game.Contracts.csproj
COPY src/Services/GameService/Core/Game.Domain/Game.Domain.csproj src/Services/GameService/Core/Game.Domain/Game.Domain.csproj
COPY src/Services/GameService/Core/Game.Application/Game.Application.csproj src/Services/GameService/Core/Game.Application/Game.Application.csproj
COPY src/Services/GameService/Infrastructure/Game.Persistence/Game.Persistence.csproj src/Services/GameService/Infrastructure/Game.Persistence/Game.Persistence.csproj
COPY src/Services/GameService/Presentation/Game.API/Game.API.csproj src/Services/GameService/Presentation/Game.API/Game.API.csproj
COPY src/Services/OrderService/Core/Order.Domain/Order.Domain.csproj src/Services/OrderService/Core/Order.Domain/Order.Domain.csproj
COPY src/Services/OrderService/Core/Order.Application/Order.Application.csproj src/Services/OrderService/Core/Order.Application/Order.Application.csproj
COPY src/Services/OrderService/Infrastructure/Order.Persistence/Order.Persistence.csproj src/Services/OrderService/Infrastructure/Order.Persistence/Order.Persistence.csproj
COPY src/Services/OrderService/Presentation/Order.API/Order.API.csproj src/Services/OrderService/Presentation/Order.API/Order.API.csproj
COPY src/Services/SearchService/Search.API/Search.API.csproj src/Services/SearchService/Search.API/Search.API.csproj
COPY src/Services/FilterService/Filter.API/Filter.API.csproj src/Services/FilterService/Filter.API/Filter.API.csproj
COPY src/Services/BasketService/Basket.API/Basket.API.csproj src/Services/BasketService/Basket.API/Basket.API.csproj
COPY src/Services/DiscountService/Discount.API/Discount.API.csproj src/Services/DiscountService/Discount.API/Discount.API.csproj
COPY src/Services/IdentityService/IdentityService.csproj src/Services/IdentityService/IdentityService.csproj
COPY src/ApiGateways/ApiGateway.YARP/ApiGateway.YARP.csproj src/ApiGateways/ApiGateway.YARP/ApiGateway.YARP.csproj

# Restore dependencies
RUN dotnet restore GameMarket.sln

# Copy the rest of the source code and build the project
COPY src/Services/SearchService/Search.API src/Services/SearchService/Search.API
COPY src/Contracts/Game.Contracts src/Contracts/Game.Contracts
WORKDIR /app/src/Services/SearchService/Search.API
RUN dotnet publish -c Release -o /app/src/out

# Second Stage: Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/src/out .
ENTRYPOINT ["dotnet", "Search.API.dll"]
