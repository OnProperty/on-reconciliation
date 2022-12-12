using System.Data;
using System.Data.SqlClient;
using On.Reconciliation.Core.Queries;

namespace On.Reconciliation.Api.UnitTests;

public class MatchingTests
{
    private const string BankAccount = "15032928401";
    
    private readonly IStatementQueries _statementQueries;
    private readonly IGeneralLedgerQueries _generalLedgerQueries;

    public MatchingTests()
    {
        var connection = new SqlConnection(ConnectionHelper.GetConnectionString);

        _statementQueries = new StatementQueries(connection);
        _generalLedgerQueries = new GeneralLedgerQueries(connection);
    }

    [Fact]
    public void Foo()
    {
        var statements = _statementQueries.GetAllUnmatchedEntries(BankAccount);
        var ledgerEntries = _generalLedgerQueries.GetBookEntriesForSingleDay(statements.First().Timestamp, BankAccount);
    }
}