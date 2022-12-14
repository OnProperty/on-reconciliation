using System.Data.SqlClient;
using FluentAssertions;
using On.Reconciliation.Core.Queries;

namespace On.Reconciliation.Api.UnitTests;

public class StatementQueriesTests
{
    private SqlConnection _connection;
    private readonly StatementQueries _queries;

    public StatementQueriesTests()
    {
        _connection = new SqlConnection(ConnectionHelper.GetConnectionString);
        _queries = new StatementQueries(_connection);
    }

    [Fact]
    public void Can_get_statements()
    {
        var statements = _queries.GetAllUnmatchedEntries("15032928401");
        statements.Count().Should().Be(34);
    }
}