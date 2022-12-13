using System.Data;
using Dapper;
using On.Reconciliation.Models.Database;

namespace On.Reconciliation.Core.Queries;

public interface IStatementQueries
{
    public IEnumerable<EC_BankStatementEntry> GetAllUnmatchedEntries(string bankAccount);
    IEnumerable<EC_BankStatementEntry> GetEntriesForDate(string bankAccount, DateOnly date);
}

public class StatementQueries : IStatementQueries
{
    private readonly IDbConnection _connection;

    public StatementQueries(IDbConnection connection)
    {
        _connection = connection;
    }
    
    //TODO: set up db schema versioning for reconciliation tables
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

    public IEnumerable<EC_BankStatementEntry> GetEntriesForDate(string bankAccount, DateOnly date)
    {
        var query = "SELECT * FROM EC_BankStatementEntry WHERE BankAccount = @bankAccount AND Timestamp >= @date AND Timestamp <= @dayAfter";

        var result = _connection.Query<EC_BankStatementEntry>(query,
            new
            {
                bankAccount, 
                date = date.ToDateTime(TimeOnly.MinValue),
                dayAfter = date.AddDays(1).ToDateTime(TimeOnly.MinValue)
            });

        return result;
    }
}