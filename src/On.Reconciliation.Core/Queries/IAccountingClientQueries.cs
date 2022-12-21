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
        throw new NotImplementedException();
    }

    public string[] GetBankAccounts(int accountingClientId)
    {
        throw new NotImplementedException();
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