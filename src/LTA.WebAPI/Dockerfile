FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/LTA.WebAPI/LTA.WebAPI.csproj", "src/LTA.WebAPI/"]
RUN dotnet restore "src/LTA.WebAPI/LTA.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/LTA.WebAPI"
RUN dotnet build "LTA.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LTA.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LTA.WebAPI.dll"]
