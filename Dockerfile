FROM node:20-alpine as node
WORKDIR /src
RUN npm install -g npm@10.2.4 -f

COPY ClientApp/package*.json ./
RUN npm install -f

COPY ClientApp/ .
RUN npm run build-only

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish -c Release -o /app/build

FROM base AS final
WORKDIR /app
COPY --from=build /app/build .
COPY --from=node /src/dist ./wwwroot

ENTRYPOINT ["dotnet", "HackTonTemplate.dll"]