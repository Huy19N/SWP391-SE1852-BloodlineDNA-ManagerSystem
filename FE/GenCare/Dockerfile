# syntax=docker/dockerfile:1

# ----------- Build Stage -----------
ARG NODE_VERSION=22.13.1
FROM node:${NODE_VERSION}-slim AS builder
WORKDIR /app

# Install dependencies with cache and bind mounts for deterministic builds
COPY --link package.json package-lock.json ./
RUN --mount=type=cache,target=/root/.npm \
    npm ci

# Copy the rest of the application (excluding files in .dockerignore)
COPY --link . .

# Build the production static files
RUN --mount=type=cache,target=/root/.npm \
    npm run build

# ----------- Production Stage -----------
FROM nginx:alpine AS final

# Security: create a non-root user and use it
RUN addgroup -S gencare && adduser -S gencare -G gencare

# Copy built static files from builder
COPY --from=builder /app/dist /usr/share/nginx/html

# Expose the default HTTP port
EXPOSE 80

USER gencare

CMD ["nginx", "-g", "daemon off;"]
