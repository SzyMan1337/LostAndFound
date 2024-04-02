#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProfileService/src/LostAndFound.ProfileService/LostAndFound.ProfileService.csproj", "ProfileService/src/LostAndFound.ProfileService/"]
COPY ["ProfileService/src/LostAndFound.ProfileService.DataAccess/LostAndFound.ProfileService.DataAccess.csproj", "ProfileService/src/LostAndFound.ProfileService.DataAccess/"]
COPY ["ProfileService/src/LostAndFound.ProfileService.Core/LostAndFound.ProfileService.Core.csproj", "ProfileService/src/LostAndFound.ProfileService.Core/"]
COPY ["ProfileService/src/LostAndFound.ProfileService.ThirdPartyServices/LostAndFound.ProfileService.ThirdPartyServices.csproj", "ProfileService/src/LostAndFound.ProfileService.ThirdPartyServices/"]
COPY ["ProfileService/src/LostAndFound.ProfileService.CoreLibrary/LostAndFound.ProfileService.CoreLibrary.csproj", "ProfileService/src/LostAndFound.ProfileService.CoreLibrary/"]
RUN dotnet restore "ProfileService/src/LostAndFound.ProfileService/LostAndFound.ProfileService.csproj"
COPY . .
WORKDIR "/src/ProfileService/src/LostAndFound.ProfileService"
RUN dotnet build "LostAndFound.ProfileService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LostAndFound.ProfileService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LostAndFound.ProfileService.dll"]
