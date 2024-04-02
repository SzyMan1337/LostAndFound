#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PublicationService/src/LostAndFound.PublicationService/LostAndFound.PublicationService.csproj", "PublicationService/src/LostAndFound.PublicationService/"]
COPY ["PublicationService/src/LostAndFound.PublicationService.Core/LostAndFound.PublicationService.Core.csproj", "PublicationService/src/LostAndFound.PublicationService.Core/"]
COPY ["PublicationService/src/LostAndFound.PublicationService.CoreLibrary/LostAndFound.PublicationService.CoreLibrary.csproj", "PublicationService/src/LostAndFound.PublicationService.CoreLibrary/"]
COPY ["PublicationService/src/LostAndFound.PublicationService.DataAccess/LostAndFound.PublicationService.DataAccess.csproj", "PublicationService/src/LostAndFound.PublicationService.DataAccess/"]
COPY ["PublicationService/src/LostAndFound.PublicationService.ThirdPartyServices/LostAndFound.PublicationService.ThirdPartyServices.csproj", "PublicationService/src/LostAndFound.PublicationService.ThirdPartyServices/"]
RUN dotnet restore "PublicationService/src/LostAndFound.PublicationService/LostAndFound.PublicationService.csproj"
COPY . .
WORKDIR "/src/PublicationService/src/LostAndFound.PublicationService"
RUN dotnet build "LostAndFound.PublicationService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LostAndFound.PublicationService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LostAndFound.PublicationService.dll"]
