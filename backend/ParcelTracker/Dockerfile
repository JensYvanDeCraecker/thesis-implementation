FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

COPY ./ParcelTracker.csproj ./
RUN dotnet restore "ParcelTracker.csproj"

COPY . .
RUN dotnet build -c Release
RUN dotnet publish -c Release -o /app --no-restore --no-build

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "ParcelTracker.dll"]