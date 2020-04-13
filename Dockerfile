# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

# copy everything else and build app
COPY . .
RUN dotnet restore
RUN dotnet publish -c release -o /app ./MyMusic.Api/MyMusic.Api.csproj

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "MyMusic.Api.dll"]
