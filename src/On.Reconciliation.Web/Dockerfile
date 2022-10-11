FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /build
COPY [ "On.Reconciliation.Web.csproj", "." ]
RUN dotnet restore "On.Reconciliation.Web.csproj"
COPY . . 
RUN dotnet publish -o publish -c Release

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=build /build/publish .
ENTRYPOINT ["dotnet", "On.Reconciliation.Web.dll"]

