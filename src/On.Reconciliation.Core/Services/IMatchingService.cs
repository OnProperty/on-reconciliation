using On.Reconciliation.Core.Queries;

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
        var unmatchedEntryGroups = _statementQueries.GetAllUnmatchedEntries(bankAccount).GroupBy(x => x.Timestamp.Date);

        foreach (var unmatchedEntry in unmatchedEntryGroups)
        {
            _generalLedgerQueries.GetBookEntriesForSingleDay()
        }
    }
}