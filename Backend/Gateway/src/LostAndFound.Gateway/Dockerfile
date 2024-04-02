#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Gateway/src/LostAndFound.Gateway/LostAndFound.Gateway.csproj", "Gateway/src/LostAndFound.Gateway/"]
RUN dotnet restore "Gateway/src/LostAndFound.Gateway/LostAndFound.Gateway.csproj"
COPY . .
WORKDIR "/src/Gateway/src/LostAndFound.Gateway"
RUN dotnet build "LostAndFound.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LostAndFound.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LostAndFound.Gateway.dll"]
