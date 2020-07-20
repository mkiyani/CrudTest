#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["UI/CrudTest.UI.csproj", "UI/"]
COPY ["Data Access/CrudTest.DataAccess/CrudTest.DataAccess.csproj", "Data Access/CrudTest.DataAccess/"]
COPY ["Application Core/CrudTest.Application.Core/CrudTest.Application.Core.csproj", "Application Core/CrudTest.Application.Core/"]
RUN dotnet restore "UI/CrudTest.UI.csproj"
COPY . .
WORKDIR "/src/UI"
RUN dotnet build "CrudTest.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CrudTest.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CrudTest.UI.dll"]