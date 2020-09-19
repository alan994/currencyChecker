#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CurrencyChecker.Web/CurrencyChecker.Web.csproj", "CurrencyChecker.Web/"]
COPY ["External/CurrencyChecker.External.Api.DependencyInjection/CurrencyChecker.External.Api.DependencyInjection.csproj", "External/CurrencyChecker.External.Api.DependencyInjection/"]
COPY ["External/CurrencyChecker.External.Api.Hnb/CurrencyChecker.External.Api.Hnb.csproj", "External/CurrencyChecker.External.Api.Hnb/"]
COPY ["External/CurrencyChecker.External.Api.Abstractions/CurrencyChecker.External.Api.Abstractions.csproj", "External/CurrencyChecker.External.Api.Abstractions/"]
COPY ["CurrencyChecker.BusinessLogic/CurrencyChecker.BusinessLogic.csproj", "CurrencyChecker.BusinessLogic/"]
COPY ["CurrencyChecker.Data/CurrencyChecker.Data.csproj", "CurrencyChecker.Data/"]
RUN dotnet restore "CurrencyChecker.Web/CurrencyChecker.Web.csproj"
COPY . .
WORKDIR "/src/CurrencyChecker.Web"
RUN dotnet build "CurrencyChecker.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CurrencyChecker.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CurrencyChecker.Web.dll"]
