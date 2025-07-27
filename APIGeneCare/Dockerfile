# syntax=docker/dockerfile:1

ARG DOTNET_VERSION=8.0

###########################
# Build Stage
###########################
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
WORKDIR /src

# Copy csproj and restore as distinct layers for better caching
COPY --link APIGeneCare.csproj ./
RUN --mount=type=cache,target=/root/.nuget/packages \
    --mount=type=cache,target=/root/.cache/msbuild \
    dotnet restore

# Copy the rest of the source code
COPY --link . .

# Publish the application
RUN dotnet publish -c Release -o /app/publish --no-restore

###########################
# Runtime Stage
###########################
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS final
WORKDIR /app

# Create a non-root user and switch to it
RUN addgroup --system appgroup && adduser --system --ingroup appgroup appuser
USER appuser

COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "APIGeneCare.dll"]
