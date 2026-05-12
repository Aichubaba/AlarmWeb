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
