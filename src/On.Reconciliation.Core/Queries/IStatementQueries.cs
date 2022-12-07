using System.Data;
using Dapper;
using On.Reconciliation.Models.Database;

namespace On.Reconciliation.Core.Queries;

public interface IStatementQueries
{
    public IEnumerable<(EC_BankStatementEntry? StatementEntry, EC_AccountCurrentBook? BookEntry)> GetAllUnmatchedEntries(string bankAccount);
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
    public IEnumerable<(EC_BankStatementEntry? StatementEntry, EC_AccountCurrentBook? BookEntry)> GetAllUnmatchedEntries(string bankAccount)
    {
        var query = @"SELECT TOP 30 bse.*, bs.*,  FROM EC_BankStatementEntry bse
                      JOIN EC_ReconciliationStatus rs ON rs.BankStatementEntryId = bse.Id
                      JOIN EC_BankStatement bs ON bs.Id = bse.BankStatementId
                      JOIN EC_AccountCurrentBook acb ON acb.CurrentBookId = rs.CurrentBookId
                      WHERE rs.Reconciliated = 0"; //TODO: filter on accountingclient

        var result = _connection.Query<EC_BankStatementEntry?, EC_AccountCurrentBook?, (EC_BankStatementEntry?, EC_AccountCurrentBook?)>(
            query,
            map: (statementEntry, bookEntry) => (statementEntry, bookEntry), // maps the two objects into a Tuple
            param: new {});

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