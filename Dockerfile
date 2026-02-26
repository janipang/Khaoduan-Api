FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["khaoduan-api.csproj", "."]
RUN dotnet restore

COPY . .
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "khaoduan-api.dll"]

# docker network create khaoduan-net

# docker build -t khaoduan-fe-image . 
# docker container rm -f khaoduan-web
# docker run -d --name khaoduan-web --network khaoduan-net -p 3000:3000 -e NEXT_PUBLIC_API_URL=http://khaoduan-api:8080 khaoduan-fe-image

# docker build -t khaoduan-api . 
# docker container rm -f khaoduan-api
# docker run -d --name khaoduan-api --network khaoduan-net -p 8080:8080 -v D:/Code/Project/Web/khaoduan-api/Storage:/app/storage khaoduan-api
