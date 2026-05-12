# Документация по развертыванию

## Требования

- .NET SDK 10.0
- Docker Engine 24+
- PostgreSQL 15

---

## 1. Развертывание через Docker Compose (рекомендуется)

### Запуск

```bash
docker compose up -d
```

Стек:
- **db** — PostgreSQL 15, порт `5432`, том `pgdata` для сохранения данных
- **app** — приложение, порт `5159:80`

### Переменные окружения (app)

| Переменная                                  | Описание                        | Значение по умолчанию                            |
|---------------------------------------------|---------------------------------|--------------------------------------------------|
| `ConnectionStrings__DefaultConnection`      | Строка подключения к PostgreSQL | `Host=db;Database=postgres;Username=postgres;Password=130805` |

### Переменные окружения (db)

| Переменная       | Описание          | Значение по умолчанию |
|------------------|-------------------|-----------------------|
| `POSTGRES_USER`  | Пользователь БД   | `postgres`            |
| `POSTGRES_PASSWORD` | Пароль         | `130805`              |
| `POSTGRES_DB`    | Имя базы данных   | `postgres`            |

### Остановка

```bash
docker compose down          # остановить контейнеры
docker compose down -v       # остановить и удалить том с данными
```

---

## 2. Развертывание через Docker (single container)

### Сборка образа

```bash
docker build -t blazorapp2 -f Dockerfile .
```

### Запуск контейнера

```bash
docker run -d \
  -p 5159:80 \
  -e ConnectionStrings__DefaultConnection="Host=host.docker.internal;Database=postgres;Username=postgres;Password=130805" \
  --name blazorapp2 \
  blazorapp2
```

> Убедитесь, что PostgreSQL доступен по адресу `host.docker.internal`.

---

## 3. Развертывание вручную (без Docker)

### Настройка PostgreSQL

Установите PostgreSQL 15 и создайте базу данных:

```sql
CREATE DATABASE postgres;
CREATE USER postgres WITH PASSWORD '130805';
GRANT ALL PRIVILEGES ON DATABASE postgres TO postgres;
```

### Настройка подключения

Файл `BlazorApp2/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=postgres;Username=postgres;Password=130805"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

Для среды разработки — `appsettings.Development.json`.

### Сборка и запуск

```bash
dotnet restore BlazorApp2/BlazorApp2.csproj
dotnet build BlazorApp2/BlazorApp2.csproj -c Release
dotnet publish BlazorApp2/BlazorApp2.csproj -c Release -o ./publish
dotnet ./publish/BlazorApp2.dll
```

При первом запуске миграции базы данных применяются автоматически (с retry-механизмом: до 10 попыток с интервалом 3 секунды).

### Профили запуска (launchSettings.json)

| Профиль       | URL                                        | Переменные                  |
|---------------|--------------------------------------------|-----------------------------|
| `https`       | `https://localhost:7086;http://localhost:5159` | `ASPNETCORE_ENVIRONMENT=Development` |

```bash
dotnet run --project BlazorApp2/BlazorApp2.csproj --launch-profile "https"
```

---

## 4. Конфигурация

### appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=postgres;Username=postgres;Password=130805"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### CORS

Политика `Internal` разрешает запросы с `https://internal-system.local`. Для изменения домена отредактируйте `Program.cs`:

```csharp
options.WithOrigins("https://your-domain.local")
```

---

## 5. Health Checks

Эндпоинт `/health` возвращает `200 OK` при正常工作е приложения. Может использоваться для балансировщиков нагрузки и Kubernetes probes.

---

## 6. Метрики

Эндпоинт `/metrics` — заглушка для Prometheus.

---

## 7. Структура Dockerfile

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app
COPY BlazorApp2/BlazorApp2.csproj BlazorApp2/
RUN dotnet restore BlazorApp2/BlazorApp2.csproj
COPY . .
RUN dotnet publish BlazorApp2/BlazorApp2.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "BlazorApp2.dll"]
```

Многоступенчатая сборка: на этапе `build` выполняется restore и publish, финальный образ содержит только рантайм `.NET ASP.NET 10.0`.

---

## 8. Миграции Entity Framework

Миграции применяются автоматически при запуске приложения. Для создания новой миграции:

```bash
dotnet ef migrations add MigrationName \
  --project BlazorApp2/BlazorApp2.csproj
```

Для отката:

```bash
dotnet ef migrations remove \
  --project BlazorApp2/BlazorApp2.csproj
```

---

## 9. Рекомендации для production

1. **Пароль БД**: замените `130805` на сгенерированный secure-пароль
2. **Аутентификация**: замените хардкодные учётные данные (`admin/admin123`, `user/user123`) на полноценную систему регистрации
3. **HTTPS**: настройте reverse proxy (nginx, Traefik) с TLS-терминацией
4. **Логирование**: подключите централизованное логирование (Seq, ELK)
5. **Метрики**: реализуйте полноценный `/metrics` для Prometheus + Grafana
6. **Обновления**: для обновления используйте `docker compose pull && docker compose up -d`
