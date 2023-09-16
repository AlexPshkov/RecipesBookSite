#STAGE 1
FROM node:latest AS build
WORKDIR src/app
COPY package.json package-lock.json ./
RUN npm install
COPY . .
RUN npm run build --omit=dev

#STAGE 2
FROM nginx:alpine
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=build /src/app/dist/recipes-site /app/publish/html
EXPOSE 80
ENTRYPOINT ["nginx", "-g", "daemon off;"]
