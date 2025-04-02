# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project file and restore dependencies
COPY WeatherFrontend.csproj ./
RUN dotnet restore WeatherFrontend.csproj

# Copy all files and publish the app
COPY . .
RUN dotnet publish -c Release -o /app/out

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/out .

# Expose port 80 (Render’s default port)
EXPOSE 80

# Set the ASPNETCORE_URLS environment variable to bind to port 80
ENV ASPNETCORE_URLS=http://+:80

# Start the .NET application (replace WeatherFrontend.dll with your actual DLL name if different)
ENTRYPOINT ["dotnet", "WeatherFrontend.dll"]