# ============================================
# STAGE 1 — Build the application
# ============================================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["CurrencyDashboard.csproj", "./"]
RUN dotnet restore "CurrencyDashboard.csproj"

# Copy everything else and build
COPY . .
RUN dotnet publish "CurrencyDashboard.csproj" -c Release -o /app/publish

# ============================================
# STAGE 2 — Run the application
# ============================================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080

# Railway expects the app to run on port 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "CurrencyDashboard.dll"]
