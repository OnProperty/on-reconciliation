using System.Net.Http.Json;
using On.Reconciliation.Models.Database;

namespace On.Reconciliation.Web.Services;

public interface IAccountService
{
    public int[] GetAccountNumbers();
}

public class AccountService : IAccountService
{
    private readonly HttpClient _client;

    public AccountService(HttpClient client)
    {
        _client = client;
    }
    
    private int[]? _accountNumbers;
    
    public int[] GetAccountNumbers()
    {
        if (_accountNumbers == null)
            Fetch();

        return _accountNumbers ?? Array.Empty<int>();
    }

    private void Fetch()
    {
        Console.WriteLine("Getting account numbers from api");
        var response = _client.GetFromJsonAsync<int[]>("reconciliation/AccountNumbers");
        response.Wait();
        _accountNumbers = response.Result;
    }
}