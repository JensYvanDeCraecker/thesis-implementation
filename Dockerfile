FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-backend
WORKDIR /source
COPY ./backend/ParcelTracker/ParcelTracker.csproj ./
RUN dotnet restore "ParcelTracker.csproj"
COPY ./backend/ParcelTracker .
RUN dotnet build -c Release
RUN dotnet publish -c Release -o /app --no-restore --no-build

FROM node:15.11 AS build-frontend
WORKDIR /app
COPY ./frontend/package*.json ./
RUN npm ci
COPY ./frontend .
RUN npm run build

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-backend /app ./
COPY --from=build-frontend ./app/out ./wwwroot
ENTRYPOINT ["dotnet", "ParcelTracker.dll"]