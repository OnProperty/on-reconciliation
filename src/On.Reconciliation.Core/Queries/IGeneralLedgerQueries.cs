using System.Data;
using Dapper;
using On.Reconciliation.Models.Database;

namespace On.Reconciliation.Core.Queries;

public interface IGeneralLedgerQueries
{
    IEnumerable<EC_GeneralLedger> GetBookEntriesForSingleDay(DateOnly date, string bankAccount);
    IEnumerable<EC_GeneralLedger> GetBookEntriesForSingleDay(DateTime date, string bankAccount);
}

public class GeneralLedgerQueries : IGeneralLedgerQueries
{
    private readonly IDbConnection _connection;

    public GeneralLedgerQueries(IDbConnection connection)
    {
        _connection = connection;
    }

    public IEnumerable<EC_GeneralLedger> GetBookEntriesForSingleDay(DateTime dateTime, string bankAccount)
    {
        var day = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
        return GetBookEntriesForSingleDay(day, bankAccount);
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