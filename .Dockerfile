FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ../*.sln ./
COPY ../DigitalWallet.Application/DigitalWallet.Application.csproj DigitalWallet.Application/
COPY ../DigitalWallet.Domain/DigitalWallet.Domain.csproj DigitalWallet.Domain/
COPY ../DigitalWallet.Infrastructure/DigitalWallet.Infrastructure.csproj DigitalWallet.Infrastructure/
COPY ../DigitalWallet.Presentation/DigitalWallet.Presentation.csproj DigitalWallet.Presentation/

RUN dotnet restore

COPY ../DigitalWallet.Application/ DigitalWallet.Application/
COPY ../DigitalWallet.Domain/ DigitalWallet.Domain/
COPY ../DigitalWallet.Infrastructure/ DigitalWallet.Infrastructure/
COPY ../DigitalWallet.Presentation/ DigitalWallet.Presentation/

WORKDIR /src/DigitalWallet.Presentation

RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

COPY DigitalWallet.Presentation/appsettings.json .

ENTRYPOINT ["dotnet", "DigitalWallet.Presentation.dll"]