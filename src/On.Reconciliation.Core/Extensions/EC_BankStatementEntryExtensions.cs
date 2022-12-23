using On.Reconciliation.Models.Database;
using OnProperty.Messaging.Internal.Reconciliation.Commands;

namespace On.Reconciliation.Core.Extensions;

public static class EC_BankStatementEntryExtensions
{
    public static bool TryMatchAnyRule(this EC_BankStatementEntry entry, List<EC_ReconciliationRules> rules, out EC_ReconciliationRules? matchedRule)
    {
        // IRuleQueries.GetAllRulesForBankAccount already filters for accountingClient, so we don't recheck
        // if AccountingClientId is null, the rule is "global". Check non-global rules first, then global ones 
        if (MatchRules(entry, rules.Where(x => x.AccountingClientId.HasValue).ToList(), out matchedRule)) return true;
        if (MatchRules(entry, rules.Where(x => x.AccountingClientId == null).ToList(), out matchedRule)) return true;

        return false;
    }

    private static bool MatchRules(EC_BankStatementEntry entry, List<EC_ReconciliationRules> rules, out EC_ReconciliationRules? matchedRule)
    {
        var matchedRules = rules.Where(entry.MatchesRule).ToList();
        if (matchedRules.Count > 1)
            throw new Exception($"Entry {entry.Id} matched {matchedRules.Count} rules");
        if (matchedRules.Count == 1)
        {
            matchedRule = matchedRules.Single();
            return true;
        }

        matchedRule = null;
        return false;
    }

    public static bool MatchesRule(this EC_BankStatementEntry bankStatementEntry, EC_ReconciliationRules rule)
    {
        if (bankStatementEntry.AdditionalInfo != null && bankStatementEntry.AdditionalInfo.Contains(rule.ContainsDescription))
            return true;

        return false;
    }
}