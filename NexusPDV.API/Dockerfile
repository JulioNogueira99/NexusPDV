# 1. Imagem de Build (SDK do .NET 9)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Arquivos de projeto
COPY ["NexusPDV.API/NexusPDV.API.csproj", "NexusPDV.API/"]
COPY ["NexusPDV.Application/NexusPDV.Application.csproj", "NexusPDV.Application/"]
COPY ["NexusPDV.Domain/NexusPDV.Domain.csproj", "NexusPDV.Domain/"]
COPY ["NexusPDV.Infrastructure/NexusPDV.Infrastructure.csproj", "NexusPDV.Infrastructure/"]

# Dependências
RUN dotnet restore "NexusPDV.API/NexusPDV.API.csproj"

COPY . .
WORKDIR "/src/NexusPDV.API"
RUN dotnet build "NexusPDV.API.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "NexusPDV.API.csproj" -c Release -o /app/publish

# 2. Imagem de Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NexusPDV.API.dll"]