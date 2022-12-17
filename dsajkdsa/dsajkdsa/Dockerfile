FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["dsajkdsa/dsajkdsa.csproj", "dsajkdsa/"]
RUN dotnet restore "dsajkdsa/dsajkdsa.csproj"
COPY . .
WORKDIR "/src/dsajkdsa"
RUN dotnet build "dsajkdsa.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "dsajkdsa.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "dsajkdsa.dll"]
