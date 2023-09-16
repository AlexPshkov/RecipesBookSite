FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5204

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Web.Api/Web.Api.csproj", "Web.Api/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Services/Services.csproj", "Services/"]
RUN dotnet restore "Web.Api/Web.Api.csproj"
COPY . .
WORKDIR "/src/Web.Api"
RUN dotnet build "Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

VOLUME wwwroot /app/wwwroot

FROM base AS final
WORKDIR /app
ENV URLS http://*:5204/
ENV ASPNETCORE_URLS http://*:5204/
ENV DB_SERVER recipes-site-app-db
ENV DB_PORT 5432
ENV DB_USER alexpshkov
ENV DB_PASSWORD jbierce123
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.Api.dll"]
