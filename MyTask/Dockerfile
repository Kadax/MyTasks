# Stage 1: Сборка приложения
FROM node:20-alpine AS build
WORKDIR /app


COPY package*.json ./

RUN npm install -g @angular/cli@17
RUN npm ci
COPY . .
RUN ng build


# Stage 2: Запуск nginx
FROM nginx:alpine

# Копируем собранное приложение из предыдущего этапа в рабочую директорию nginx
COPY --from=build /app/dist/MyTask/browser /usr/share/nginx/html

# Меняем конфиг nginx-а на собственный
COPY --from=build /app/nginx.conf /etc/nginx/nginx.conf

EXPOSE 8080

# Запускаем nginx
CMD ["nginx", "-g", "daemon off;"]
