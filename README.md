# BlazorApp2

Веб-приложение для мониторинга сетевых устройств, управления инцидентами и просмотра отчётов.

## Возможности

- **Дашборд** — сводная статистика по узлам, тикетам и последним отчётам
- **Мониторинг** — просмотр последних отчётов по каждому узлу с детализацией
- **Тикет-система** — создание, закрытие, смена статусов, история изменений, массовые операции
- **Оборудование** — каталог оборудования с сетевыми параметрами
- **Схемы** — загрузка и просмотр схем узлов (галерея с фильтрацией)
- **Аутентификация** — ролевая модель (администратор / пользователь)
- **Алармы** — REST API для приёма внешних алармов

## Технологии

| Компонент          | Технология                              |
|--------------------|-----------------------------------------|
| Фреймворк          | .NET 10 (Blazor Interactive Server)     |
| База данных        | PostgreSQL 15                           |
| ORM                | Entity Framework Core 10                |
| Аутентификация     | Cookie-based + ClaimsPrincipal          |
| API                | REST (Controllers) + Swagger            |
| Фронтенд           | Bootstrap 5, Chart.js, Bootstrap Icons  |
| Контейнеризация    | Docker / Docker Compose                 |

## Быстрый старт

```bash
git clone https://github.com/Aichubaba/AlarmWeb.git
cd BlazorApp2

# Запуск через Docker Compose
docker compose up -d

# Открыть в браузере
# http://localhost:5159
```

Учётные данные по умолчанию:

| Логин | Пароль   | Роль  |
|-------|----------|-------|
| admin | admin123 | admin |
| user  | user123  | user  |

## Структура проекта

```
BlazorApp2/
├── BlazorApp2/                  # Основное приложение
│   ├── Components/              # Blazor-компоненты и страницы
│   │   ├── Layout/              # Компоновки (MainLayout, NavMenu, EmptyLayout)
│   │   └── Pages/               # Страницы (Home, Login, Tickets, Nodes и др.)
│   ├── Controllers/             # REST API контроллеры
│   ├── Data/                    # DbContext и миграции EF Core
│   ├── Helpers/                 # Вспомогательные классы
│   ├── Models/                  # Модели данных
│   ├── Services/                # Бизнес-логика (сервисы)
│   └── wwwroot/                 # Статические файлы (CSS, JS)
├── BlazorApp2.Tests/            # Модульные и интеграционные тесты
├── docs/                        # Документация
├── compose.yaml                 # Docker Compose конфигурация
└── Dockerfile                   # Dockerfile
```

## Документация

- [Руководство пользователя](docs/user-guide.md)
- [API документация](docs/api-documentation.md)
- [Документация по развертыванию](docs/deployment-guide.md)

## Тестирование

```bash
dotnet test BlazorApp2.Tests/BlazorApp2.Tests.csproj
```

## Лицензия

MIT
