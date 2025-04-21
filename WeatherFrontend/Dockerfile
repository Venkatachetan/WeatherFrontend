# Use the official .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set working directory
WORKDIR /app

# Copy only the .csproj and restore
COPY WeatherFrontend/WeatherFrontend.csproj ./WeatherFrontend/
RUN dotnet restore WeatherFrontend/WeatherFrontend.csproj

# Copy the rest of the project files
COPY WeatherFrontend/. ./WeatherFrontend/

# Set working directory to project folder
WORKDIR /app/WeatherFrontend

# Build the application
RUN dotnet publish -c Release -o out
