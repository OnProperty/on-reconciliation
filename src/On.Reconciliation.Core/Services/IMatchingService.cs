using On.Reconciliation.Core.Extensions;
using On.Reconciliation.Core.Queries;
using On.Reconciliation.Models.Database;

namespace On.Reconciliation.Core.Services;

public interface IMatchingService
{
    Task AutomatchForBankAccount(string bankAccount);
}

public class MatchingService : IMatchingService
{
    private readonly IGeneralLedgerQueries _generalLedgerQueries;
    private readonly IStatementQueries _statementQueries;

    public MatchingService(IGeneralLedgerQueries generalLedgerQueries, IStatementQueries statementQueries)
    {
        _generalLedgerQueries = generalLedgerQueries;
        _statementQueries = statementQueries;
    }
    
    public Task AutomatchForBankAccount(string bankAccount)
    {
        var unmatchedEntries = _statementQueries.GetAllUnmatchedEntries(bankAccount);
        var unmatchedEntryGroups = unmatchedEntries
            .GroupBy(x => x.Timestamp.Date)
            .OrderBy(x => x.Key);

        foreach (var unmatchedEntryGroup in unmatchedEntryGroups)
        {
            var ledgerEntries = _generalLedgerQueries.GetBookEntriesForSingleDay(unmatchedEntryGroup.Key, bankAccount);

            FindMatches(unmatchedEntryGroup, ledgerEntries);
        }

        return Task.CompletedTask;
    }

    private static void FindMatches(IGrouping<DateTime, EC_BankStatementEntry> unmatchedEntryGroup, IEnumerable<EC_GeneralLedger> ledgerEntries)
    {
        foreach (var unmatchedEntry in unmatchedEntryGroup.ToList())
        {
            // single match
            var single = ledgerEntries.SingleOrDefault(x =>
                x.AmountLocalCurrency.EqualsApproximately(unmatchedEntry.Amount));
            if (single != null)
            {
                //return
            }
            else
            {
                // find sum
            }
        }
    }
}