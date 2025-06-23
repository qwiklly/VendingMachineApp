# Vending Machine App

## Описание

Vending Machine App — это полнофункциональное веб-приложение для выбора и оформления заказа на газированные напитки.  
Состоит из двух частей:

- **Backend:** ASP.NET Core 8 с базой данных PostgreSQL
- **Frontend:** React 19 + TypeScript

## Функционал

- Просмотр каталога напитков
- Фильтрация по бренду и ценовому диапазону
- Добавление/удаление напитков в корзину
- Подсчёт итоговой суммы
- Swagger UI для тестирования API

## 🛠️ Технологии

### Backend

- ASP.NET Core 8 (Web API)
- Entity Framework Core 8
- PostgreSQL
- Serilog (логирование)
- Swagger (документация)
- C#

### Frontend

- React 19
- React Router DOM
- Axios
- TypeScript
- CSS-модули

## 🧪 Установка и запуск

### 1. Backend

#### Требования

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- PostgreSQL

#### Шаги

1. Создать базу данных PostgreSQL и пользователя:
   ```sql
   CREATE DATABASE vending_machine_db;
   CREATE USER vending_user WITH PASSWORD 'vending123@';
   GRANT ALL PRIVILEGES ON DATABASE vending_machine_db TO vending_user;
