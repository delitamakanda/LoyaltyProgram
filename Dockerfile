FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . . 

RUN dotnet restore
RUN dotnet publish src/LoyaltyProgram.Api/LoyaltyProgram.Api.csproj -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /out .

EXPOSE 5000

ENTRYPOINT ["dotnet", "LoyaltyProgram.Api.dll"]
