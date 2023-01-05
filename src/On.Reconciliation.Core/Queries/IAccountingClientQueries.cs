using System.Data;
using Dapper;

namespace On.Reconciliation.Core.Queries;

public interface IAccountingClientQueries
{
    int[] GetAccountingClientsForUser(string userId);
    string[] GetBankAccounts(int accountingClientId);
    int GetByBankAccount(string bankAccount);
}

public class AccountingClientQueries : IAccountingClientQueries
{
    private readonly IDbConnection _connection;

    public AccountingClientQueries(IDbConnection connection)
    {
        _connection = connection;
    }
    
    public int[] GetAccountingClientsForUser(string userId)
    {
        return new[] {1, 3}; //TODO: actual query
    }

    public string[] GetBankAccounts(int accountingClientId)
    {
        var query = @"SELECT ba.BankAccount
                        FROM EC_BankAccount ba
                        JOIN EC_AccountingClient ac ON ac.ContactId = ba.ContactId
                        WHERE ac.AccountingClientId = @accountingClientId";
        return _connection.Query<string>(query, new { accountingClientId }).ToArray();
    }

    public int GetByBankAccount(string bankAccount)
    {
        var query = @"SELECT ac.AccountingClientId
                        FROM EC_AccountingClient ac
                        JOIN EC_BankAccount ba ON ba.ContactId = ac.ContactID
                        WHERE ba.BankAccount = @bankAccount";
        return _connection.QuerySingle<int>(query, new {bankAccount});
    }
}