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

# docker build -t khaoduan-api . 
# docker run -p 8080:8080 -d -v D:/Code/Project/Web/khaoduan-api/Storage:/app/atorage --name khaoduan-container khaoduan-api