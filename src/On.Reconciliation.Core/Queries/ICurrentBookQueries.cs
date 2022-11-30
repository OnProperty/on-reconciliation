using System.Data;
using Dapper;
using On.Reconciliation.Models.Database;

namespace On.Reconciliation.Core.Queries;

public interface ICurrentBookQueries
{
    IEnumerable<EC_AccountCurrentBook> GetBookEntries(DateOnly date, string bankAccount);
}

public class CurrentBookQueries : ICurrentBookQueries
{
    private readonly IDbConnection _connection;

    public CurrentBookQueries(IDbConnection connection)
    {
        _connection = connection;
    }
    
    public IEnumerable<EC_AccountCurrentBook> GetBookEntries(DateOnly date, string bankAccount)
    {
        var account = 0; // TODO: get account by bankAccount
        
        var query = @"SELECT * FROM EC_AccountCurrentBook 
         WHERE AccountId = @accountId
         AND VoucherDate = @date"; // TODO: voucherdate?

        return _connection.Query<EC_AccountCurrentBook>(query, new { date, account });
    }
}