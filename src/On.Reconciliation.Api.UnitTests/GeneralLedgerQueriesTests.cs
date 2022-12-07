using System.Data.SqlClient;
using FluentAssertions;
using On.Reconciliation.Core.Queries;

namespace On.Reconciliation.Api.UnitTests;

public class GeneralLedgerQueriesTests
{
    private SqlConnection _connection;
    
    public GeneralLedgerQueriesTests()
    {
        _connection = new(ConnectionHelper.GetConnectionString);
    }

    [Fact]
    public void Gets_Ledger_Entries_for_a_single_day()
    {
        
        var queries = new GeneralLedgerQueries(_connection);
        var result = queries.GetBookEntriesForSingleDay(new DateOnly(2013, 06, 14), "97221427886").ToArray();
        result.Count().Should().Be(1);
        result[0].AmountLocalCurrency.Should().Be(200);
    }
}