using On.Reconciliation.Core.Commands;
using On.Reconciliation.Core.Extensions;
using On.Reconciliation.Core.Queries;

namespace On.Reconciliation.Core.Services;

public interface IRuleService
{
    public int RunAllRules(string bankAccount);
}

public class RuleService : IRuleService
{
    private readonly IStatementQueries _statementQueries;
    private readonly IRuleQueries _ruleQueries;
    private readonly IBookingCommands _bookingCommands;

    public RuleService(IStatementQueries statementQueries, IRuleQueries ruleQueries, IBookingCommands bookingCommands)
    {
        _statementQueries = statementQueries;
        _ruleQueries = ruleQueries;
        _bookingCommands = bookingCommands;
    }


    public int RunAllRules(string bankAccount)
    {
        var statementEntries = _statementQueries.GetAllUnmatchedEntries(bankAccount);
        var rules = _ruleQueries.GetAllRulesForBankAccount(bankAccount);
        var ruleCount = 0;
        
        foreach (var entry in statementEntries)
        {
            if (entry.TryMatchAnyRule(rules, out var matchedRule))
            {
                _bookingCommands.BookStatementByRule(entry, matchedRule!);
                ruleCount++;
            }
        }

        return ruleCount;
    }
}