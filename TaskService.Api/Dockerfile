# Imagen base para runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

# Imagen para build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar csproj y restaurar
COPY ["TaskService.Api/TaskService.Api.csproj", "TaskService.Api/"]
COPY ["TaskService.Application/TaskService.Application.csproj", "TaskService.Application/"]
COPY ["TaskService.Domain/TaskService.Domain.csproj", "TaskService.Domain/"]
COPY ["TaskService.Infrastructure/TaskService.Infrastructure.csproj", "TaskService.Infrastructure/"]

RUN dotnet restore "TaskService.Api/TaskService.Api.csproj"

# Copiar todo el código
COPY . .
WORKDIR "/src/TaskService.Api"
RUN dotnet publish -c Release -o /app/publish

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TaskService.Api.dll"]
