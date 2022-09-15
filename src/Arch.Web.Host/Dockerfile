#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Arch.Web.Host/Arch.Web.Host.csproj", "src/Arch.Web.Host/"]
COPY ["src/Arch.Web.Core/Arch.Web.Core.csproj", "src/Arch.Web.Core/"]
COPY ["src/Arch.Application/Arch.Application.csproj", "src/Arch.Application/"]
COPY ["src/Arch.Application.Shared/Arch.Application.Shared.csproj", "src/Arch.Application.Shared/"]
COPY ["src/Arch.Core.Shared/Arch.Core.Shared.csproj", "src/Arch.Core.Shared/"]
COPY ["src/Arch.Core/Arch.Core.csproj", "src/Arch.Core/"]
COPY ["src/Arch.EntityFrameworkCore/Arch.EntityFrameworkCore.csproj", "src/Arch.EntityFrameworkCore/"]
COPY ["src/Arch.GraphQL/Arch.GraphQL.csproj", "src/Arch.GraphQL/"]
RUN dotnet restore "src/Arch.Web.Host/Arch.Web.Host.csproj"
COPY . .
WORKDIR "/src/src/Arch.Web.Host"
RUN dotnet build "Arch.Web.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Arch.Web.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Arch.Web.Host.dll"]
