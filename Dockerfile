# Étape 1 : Build .NET & TypeScript
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Installation de Node pour compiler le TS
RUN curl -fsSL https://deb.nodesource.com/setup_20.x | bash - && apt-get install -y nodejs

# Copie des fichiers projet et restauration
COPY ["src/WebApp.Client/WebApp.Client.csproj", "src/WebApp.Client/"]
COPY ["DataTable.Rcl/DataTable.Rcl.csproj", "DataTable.Rcl/"]
RUN dotnet restore "src/WebApp.Client/WebApp.Client.csproj"

# Copie du reste et compilation
COPY . .
WORKDIR "/app/src/WebApp.Client"
RUN npm install && npx tsc
RUN dotnet publish "WebApp.Client.csproj" -c Release -o /app/publish

# Étape 2 : Runtime final (plus léger)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WebApp.Client.dll"]