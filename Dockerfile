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
# docker run -p 8080:8080 -e JwtConfig__Key="YOUR_SECRET_KEY_MIN_32_CHARS!!" -e JwtConfig__Issuer="http://localhost:8080" -e JwtConfig__Audiences__0="http://localhost:8080" -e JwtConfig__TokenValidityMins="60" -e ConnectionStrings__MySql="Server=gateway01.ap-southeast-1.prod.aws.tidbcloud.com;Port=4000;Database=Khaoduan;User=45Qscwng5Xm681Z.root;Password=vxKPN03qvIZ9kqgn;SslMode=Required;" khaoduan-api