# Stage 1: Build React app
FROM node:20 AS build
WORKDIR /app

# Copy only package.json files first for better caching
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy all other source files
COPY . .

# Build the React app
RUN npm run build

# Stage 2: Serve with Nginx
FROM nginx:alpine

# Copy the build output to Nginx's HTML directory
COPY --from=build /app/build /usr/share/nginx/html

# Optional: Copy custom nginx config if needed
# COPY nginx.conf /etc/nginx/nginx.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
