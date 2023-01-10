using System.Data;
using Dapper;
using On.Reconciliation.Models.Database;
using On.Reconciliation.Models.Database.Pure;

namespace On.Reconciliation.Core.Queries;

public interface IStatementQueries
{
    public IEnumerable<EC_BankStatementEntry> GetAllUnmatchedEntries(string bankAccount);
    public IEnumerable<EntryWithStatus> GetAllEntriesForMonth(string bankAccount, int year, int month);
    public IEnumerable<string> GetAllBankAccounts();
    public BankStatementEntryExtended GetEntryById(int bankStatementEntryId);
}

public class StatementQueries : IStatementQueries
{
    private readonly IDbConnection _connection;

    public StatementQueries(IDbConnection connection)
    {
        _connection = connection;
    }
    
    public IEnumerable<EC_BankStatementEntry> GetAllUnmatchedEntries(string bankAccount)
    {
        var query = @"SELECT bse.*  FROM EC_BankStatementEntry bse
                      LEFT JOIN EC_Reconciliation rs ON rs.BankStatementEntryId = bse.Id
                      LEFT JOIN EC_BankStatement bs ON bs.Id = bse.BankStatementId
                      WHERE rs.GeneralLedgerId IS NULL
                      AND bs.BankAccount = @bankAccount";

        var result = _connection.Query<EC_BankStatementEntry>(
            query,
            param: new { bankAccount });

        return result;
    }

    public IEnumerable<EntryWithStatus> GetAllEntriesForMonth(string bankAccount, int year, int month)
    {
        var firstOfMonth = new DateTime(year, month, 1);
        var firstOfNextMonth = firstOfMonth.AddMonths(1);
        var query = @"SELECT bs.BankAccount, r.GeneralLedgerId, r.RuleId, bse.*
                      FROM EC_BankStatementEntry bse 
                      JOIN EC_BankStatement bs ON bs.Id = bse.BankStatementId
                      LEFT JOIN EC_Reconciliation r ON r.BankStatementEntryId = bse.Id
                      WHERE bs.BankAccount = @bankAccount AND bse.Timestamp >= @firstOfMonth AND bse.Timestamp < @firstOfNextMonth";
        return _connection.Query<EntryWithStatus>(query,
            new
            {
                bankAccount,
                firstOfMonth,
                firstOfNextMonth
            });
    }

    public IEnumerable<string> GetAllBankAccounts()
    {
        var query = "SELECT DISTINCT BankAccount FROM EC_BankStatement";
        return _connection.Query<string>(query);
    }

    public BankStatementEntryExtended GetEntryById(int bankStatementEntryId)
    {
        var query = @"SELECT ac.AccountingClientID, bs.BankAccount, bse.* FROM EC_BankStatementEntry bse
                        JOIN EC_BankStatement bs ON bs.Id = bse.BankStatementId
                        JOIN EC_BankAccount ba ON ba.BankAccount = bs.BankAccount
                        JOIN EC_AccountingCLient ac ON ac.ContactID = ba.ContactId
                        WHERE bse.Id = @id";
        return _connection.QuerySingle<BankStatementEntryExtended>(query, new { id = bankStatementEntryId });
    }
}