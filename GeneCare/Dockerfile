# syntax=docker/dockerfile:1

# Set the .NET version from csproj (net8.0)
ARG DOTNET_VERSION=8.0

# --- Build Stage ---
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS builder
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY --link GeneCare.csproj ./
RUN --mount=type=cache,target=/root/.nuget/packages \
    --mount=type=cache,target=/root/.cache/msbuild \
    dotnet restore "GeneCare.csproj"

# Copy the rest of the source code
COPY --link . .

# Publish the application to the /app/publish directory
RUN --mount=type=cache,target=/root/.nuget/packages \
    --mount=type=cache,target=/root/.cache/msbuild \
    dotnet publish "GeneCare.csproj" -c Release -o /app/publish --no-restore

# --- Runtime Stage ---
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS final
WORKDIR /app

# Create a non-root user and switch to it
RUN addgroup --system genegrp && adduser --system --ingroup genegrp geneuser
USER geneuser

# Copy published output from build stage
COPY --from=builder /app/publish .

# Expose the default ASP.NET Core port
EXPOSE 80

# Set the entrypoint
ENTRYPOINT ["dotnet", "GeneCare.dll"]
