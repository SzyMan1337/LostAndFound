#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ChatService/src/LostAndFound.ChatService/LostAndFound.ChatService.csproj", "ChatService/src/LostAndFound.ChatService/"]
COPY ["ChatService/src/LostAndFound.ChatService.DataAccess/LostAndFound.ChatService.DataAccess.csproj", "ChatService/src/LostAndFound.ChatService.DataAccess/"]
COPY ["ChatService/src/LostAndFound.ChatService.Core/LostAndFound.ChatService.Core.csproj", "ChatService/src/LostAndFound.ChatService.Core/"]
COPY ["ChatService/src/LostAndFound.ChatService.CoreLibrary/LostAndFound.ChatService.CoreLibrary.csproj", "ChatService/src/LostAndFound.ChatService.CoreLibrary/"]
RUN dotnet restore "ChatService/src/LostAndFound.ChatService/LostAndFound.ChatService.csproj"
COPY . .
WORKDIR "/src/ChatService/src/LostAndFound.ChatService"
RUN dotnet build "LostAndFound.ChatService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LostAndFound.ChatService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LostAndFound.ChatService.dll"]
