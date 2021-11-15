#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MTM.API/MTM.API.csproj", "MTM.API/"]
COPY ["MTM.Infra/MTM.Infra.csproj", "MTM.Infra/"]
COPY ["MTM.Domain/MTM.Domain.csproj", "MTM.Domain/"]
COPY ["MTM.Identity/MTM.Identity.csproj", "MTM.Identity/"]
COPY ["MTM.Application/MTM.Application.csproj", "MTM.Application/"]
COPY ["MTM.Cache.Redis/MTM.Cache.Redis.csproj", "MTM.Cache.Redis/"]
RUN dotnet restore "MTM.API/MTM.API.csproj"
COPY . .
WORKDIR "/src/MTM.API"
RUN dotnet build "MTM.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MTM.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MTM.API.dll"]