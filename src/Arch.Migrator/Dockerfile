#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Arch.Migrator/Arch.Migrator.csproj", "src/Arch.Migrator/"]
COPY ["src/Arch.EntityFrameworkCore/Arch.EntityFrameworkCore.csproj", "src/Arch.EntityFrameworkCore/"]
COPY ["src/Arch.Core/Arch.Core.csproj", "src/Arch.Core/"]
COPY ["src/Arch.Core.Shared/Arch.Core.Shared.csproj", "src/Arch.Core.Shared/"]
RUN dotnet restore "src/Arch.Migrator/Arch.Migrator.csproj"
COPY . .
WORKDIR "/src/src/Arch.Migrator"
RUN dotnet build "Arch.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Arch.Migrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Arch.Migrator.dll", "-s"]
