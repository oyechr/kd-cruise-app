FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["KD.Cruise.PlannedEventsApi/KD.Cruise.PlannedEventsApi.csproj", "KD.Cruise.PlannedEventsApi/"]
RUN dotnet restore "KD.Cruise.PlannedEventsApi/KD.Cruise.PlannedEventsApi.csproj"

COPY . .
WORKDIR "/src/KD.Cruise.PlannedEventsApi"
RUN dotnet publish "KD.Cruise.PlannedEventsApi.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "KD.Cruise.PlannedEventsApi.dll"]
