# Use the official .NET 6 SDK image as a build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /app

# Copy the .csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Set the environment variable for Docker.
ENV ASPNETCORE_ENVIRONMENT=Docker

# Copy everything else and build the app
COPY . ./
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Expose the port your app will run on
EXPOSE 80

# Set the entry point
ENTRYPOINT ["dotnet", "MyDockerApi.dll"]
