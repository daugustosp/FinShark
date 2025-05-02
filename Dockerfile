# Etapa base (imagem para execução)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia o arquivo .csproj
COPY api/FinShark/FinShark.csproj ./FinShark/
RUN dotnet restore "./FinShark/FinShark.csproj"

# Copia o restante dos arquivos
COPY ./api ./FinShark/

# Compila o projeto
WORKDIR /src/FinShark
RUN dotnet build "FinShark.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publicação
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FinShark.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinShark.dll"]
