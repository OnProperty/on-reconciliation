using System.Data;
using Dapper;
using On.Reconciliation.Models.Database;

namespace On.Reconciliation.Core.Queries;

public interface IGeneralLedgerQueries
{
    IEnumerable<EC_GeneralLedger> GetBookEntriesForSingleDay(DateOnly date, string bankAccount);
}

public class GeneralLedgerQueries : IGeneralLedgerQueries
{
    private readonly IDbConnection _connection;

    public GeneralLedgerQueries(IDbConnection connection)
    {
        _connection = connection;
    }
    
    public IEnumerable<EC_GeneralLedger> GetBookEntriesForSingleDay(DateOnly date, string bankAccount)
    {
        var query = @"SELECT * 
            FROM EC_GeneralLedger gl
            JOIN EC_BankAccountRelations bar ON bar.AccountId = gl.AccountID
            JOIN EC_BankAccount ba ON ba.BankAccountId = bar.BankAccountId
            WHERE ba.BankAccount = @bankAccount
                AND VoucherDate >= @dateStart
                AND VoucherDate <= @dateEnd";

        return _connection.Query<EC_GeneralLedger>(query, new { 
            dateStart = date.ToDateTime(TimeOnly.MinValue),
            dateEnd = date.ToDateTime(TimeOnly.MaxValue),
            bankAccount 
        });
    }
}