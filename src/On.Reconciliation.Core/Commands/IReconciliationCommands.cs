using System.Data;
using Dapper;
using On.Reconciliation.Core.Services;
using On.Reconciliation.Models.Database;

namespace On.Reconciliation.Core.Commands;

public interface IReconciliationCommands
{
    void InsertMatches(List<MatchResult> matches);
}

public class ReconciliationCommands : IReconciliationCommands
{
    private readonly IDbConnection _connection;

    public ReconciliationCommands(IDbConnection connection)
    {
        _connection = connection;
    }

    public void InsertMatches(List<MatchResult> matches)
    {
        foreach (var match in matches)
        {
            var query = @"INSERT INTO EC_Reconciliation(GeneralLedgerId, BankStatementEntryId, RuleId, LastChanged) VALUES(@generalLedgerId, @bankStatementEntryId, @ruleID, @lastChanged)";
            _connection.Query(query, new {match.GeneralLedgerId, match.BankStatementEntryId, match.RuleId, lastChanged = DateTime.Now});
        }
    }
}