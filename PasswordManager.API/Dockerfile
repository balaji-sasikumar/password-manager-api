FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "PasswordManager.API/PasswordManager.API.csproj"
WORKDIR "/src/PasswordManager.API"
RUN dotnet build "PasswordManager.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PasswordManager.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PasswordManager.API.dll"]