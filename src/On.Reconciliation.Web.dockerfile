FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY On.Reconciliation.Web/On.Reconciliation.Web.csproj .
RUN dotnet restore On.Reconciliation.Web.csproj
COPY . .
RUN dotnet build On.Reconciliation.Web/On.Reconciliation.Web.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish On.Reconciliation.Web/On.Reconciliation.Web.csproj -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf