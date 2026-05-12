FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Копируем файл проекта и восстанавливаем зависимости
COPY BlazorApp2/BlazorApp2.csproj BlazorApp2/
RUN dotnet restore BlazorApp2/BlazorApp2.csproj

# Копируем все исходники (включая wwwroot)
COPY . .

# Публикуем приложение
RUN dotnet publish BlazorApp2/BlazorApp2.csproj -c Release -o /app/publish

# Финальный образ только с ASP.NET Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Копируем опубликованные файлы и отдельно папку wwwroot
COPY --from=build /app/publish .
COPY --from=build /app/BlazorApp2/wwwroot ./wwwroot

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "BlazorApp2.dll"]