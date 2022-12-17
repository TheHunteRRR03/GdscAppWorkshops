FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["App_GDSC_workshops/App_GDSC_workshops.csproj", "App_GDSC_workshops/"]
RUN dotnet restore "App_GDSC_workshops/App_GDSC_workshops.csproj"
COPY . .
WORKDIR "/src/App_GDSC_workshops"
RUN dotnet build "App_GDSC_workshops.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "App_GDSC_workshops.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "App_GDSC_workshops.dll"]
