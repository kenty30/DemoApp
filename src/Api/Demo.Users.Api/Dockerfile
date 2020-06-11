#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Api/Demo.Users.Api/Demo.Users.Api.csproj", "src/Api/Demo.Users.Api/"]
COPY ["src/Api/Demo.Users.Application/Demo.Users.Application.csproj", "src/Api/Demo.Users.Application/"]
COPY ["src/Api/Demo.Users.Domain/Demo.Users.Domain.csproj", "src/Api/Demo.Users.Domain/"]
COPY ["src/Api/Demo.Users.Infrastructure/Demo.Users.Infrastructure.csproj", "src/Api/Demo.Users.Infrastructure/"]
RUN dotnet restore "src/Api/Demo.Users.Api/Demo.Users.Api.csproj"
COPY . .
WORKDIR "/src/src/Api/Demo.Users.Api"
RUN dotnet build "Demo.Users.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Demo.Users.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Demo.Users.Api.dll"]