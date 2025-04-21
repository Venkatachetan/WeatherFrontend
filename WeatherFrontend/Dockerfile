# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project file first
COPY WeatherFrontend/WeatherFrontend.csproj ./WeatherFrontend/

# Restore dependencies (caching step)
RUN dotnet restore WeatherFrontend.csproj

# Copy all files (ensure your .dockerignore isn't excluding files you need)
COPY . .

# Publish the app (adjust output directory as needed)
RUN dotnet publish -c Release -o /app/out

# Serve Stage
FROM nginx:latest
WORKDIR /usr/share/nginx/html
COPY --from=build /app/out/wwwroot .

# Expose the default nginx port
EXPOSE 80

# Start Nginx
CMD ["nginx", "-g", "daemon off;"]
