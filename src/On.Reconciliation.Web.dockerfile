FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /build
COPY . . 
RUN dotnet restore "On.Reconciliation.Web/On.Reconciliation.Web.csproj"
RUN dotnet publish On.Reconciliation.Web/On.Reconciliation.Web.csproj -o publish -c Release

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=build /build/publish .
ENTRYPOINT ["dotnet", "On.Reconciliation.Web.dll"]
