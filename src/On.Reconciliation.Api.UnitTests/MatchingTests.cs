using System.Data;
using System.Data.SqlClient;
using FluentAssertions;
using On.Reconciliation.Core.Commands;
using On.Reconciliation.Core.Queries;
using On.Reconciliation.Core.Services;

namespace On.Reconciliation.Api.UnitTests;

public class MatchingTests
{
    private const string BankAccount = "15032928401";

    private readonly StatementQueries _statementQueries;
    private readonly GeneralLedgerQueries _generalLedgerQueries;
    private readonly IMatchingService _matchingService;
    private readonly ReconciliationCommands _reconciliationCommands;

    public MatchingTests()
    {
        var connection = new SqlConnection(ConnectionHelper.GetConnectionString);

        _statementQueries = new StatementQueries(connection);
        _generalLedgerQueries = new GeneralLedgerQueries(connection);
        _reconciliationCommands = new ReconciliationCommands(connection);

        _matchingService = new MatchingService(_generalLedgerQueries, _statementQueries);
    }

    [Fact]
    public void FindsSingleMatch()
    {
        var results = _matchingService.FindMatchesForBankAccount(BankAccount);
        var oneToOne = results.Where(x => x.BankStatementEntryId == 34).ToList();
        oneToOne.Count().Should().Be(1);
        oneToOne.Single().GeneralLedgerId.Should().Be(83763);
    }
    
    [Fact]
    public void FindsMultiMatch()
    {
        var results = _matchingService.FindMatchesForBankAccount(BankAccount);
        var manyToOne = results.Where(x => x.GeneralLedgerId == 83767).OrderBy(x => x.BankStatementEntryId).ToList();
        manyToOne.Count.Should().Be(3);
        manyToOne[0].BankStatementEntryId.Should().Be(31);
        manyToOne[1].BankStatementEntryId.Should().Be(32);
        manyToOne[2].BankStatementEntryId.Should().Be(33);
    }

    [Fact]
    public void Foo()
    {
        var matches = _matchingService.FindMatchesForBankAccount(BankAccount).ToList();
        _reconciliationCommands.InsertMatches(matches);
    }
}