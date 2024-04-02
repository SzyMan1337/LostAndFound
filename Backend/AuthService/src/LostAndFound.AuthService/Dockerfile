#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AuthService/src/LostAndFound.AuthService/LostAndFound.AuthService.csproj", "AuthService/src/LostAndFound.AuthService/"]
COPY ["AuthService/src/LostAndFound.AuthService.Core/LostAndFound.AuthService.Core.csproj", "AuthService/src/LostAndFound.AuthService.Core/"]
COPY ["AuthService/src/LostAndFound.AuthService.CoreLibrary/LostAndFound.AuthService.CoreLibrary.csproj", "AuthService/src/LostAndFound.AuthService.CoreLibrary/"]
COPY ["AuthService/src/LostAndFound.AuthService.DataAccess/LostAndFound.AuthService.DataAccess.csproj", "AuthService/src/LostAndFound.AuthService.DataAccess/"]
COPY ["AuthService/src/LostAndFound.AuthService.ThirdPartyServices/LostAndFound.AuthService.ThirdPartyServices.csproj", "AuthService/src/LostAndFound.AuthService.ThirdPartyServices/"]
RUN dotnet restore "AuthService/src/LostAndFound.AuthService/LostAndFound.AuthService.csproj"
COPY . .
WORKDIR "/src/AuthService/src/LostAndFound.AuthService"
RUN dotnet build "LostAndFound.AuthService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LostAndFound.AuthService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LostAndFound.AuthService.dll"]