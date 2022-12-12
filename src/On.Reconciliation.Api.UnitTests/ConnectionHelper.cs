using Microsoft.Extensions.Configuration;

namespace On.Reconciliation.Api.UnitTests;

public static class ConnectionHelper
{
    public static string GetConnectionString =>
        new ConfigurationBuilder()
            .AddUserSecrets<JustToLocateAssembly>()
            .AddJsonFile("appsettings.json")
            .Build()
            .GetConnectionString("OnPropertyConnectionString");
}

public class JustToLocateAssembly
{
}