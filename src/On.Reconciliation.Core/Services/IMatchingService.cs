using System.Data;
using System.Text.RegularExpressions;
using On.Reconciliation.Core.Extensions;
using On.Reconciliation.Core.Queries;
using On.Reconciliation.Models.Database;
using On.Reconciliation.Models.Database.Pure;

namespace On.Reconciliation.Core.Services;

public interface IMatchingService
{
    IEnumerable<MatchResult> FindAllMatches();
    IEnumerable<MatchResult> FindMatchesForBankAccount(string bankAccount);
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
    
    public IEnumerable<MatchResult> FindAllMatches()
    {
        var result = new List<MatchResult>();
        
        var bankAccounts =  _statementQueries.GetAllBankAccounts();
        foreach (var bankAccount in bankAccounts)
        {
            result.AddRange(FindMatchesForBankAccount(bankAccount));
        }

        return result;
    }

    public IEnumerable<MatchResult> FindMatchesForBankAccount(string bankAccount)
    {
        var unmatchedEntries = _statementQueries.GetAllUnmatchedEntries(bankAccount);
        var unmatchedEntryGroups = unmatchedEntries
            .GroupBy(x => x.Timestamp.Date)
            .OrderBy(x => x.Key);

        foreach (var unmatchedEntryGroup in unmatchedEntryGroups)
        {
            var ledgerEntries = _generalLedgerQueries.GetBookEntriesForSingleDay(unmatchedEntryGroup.Key, bankAccount).ToList();

            var matches = FindMatches(unmatchedEntryGroup.ToList(), ledgerEntries);
            foreach (var match in matches)
                yield return match;
        }
    }

    private static IEnumerable<MatchResult> FindMatches(List<EC_BankStatementEntry> bankStatementEntries, List<EC_GeneralLedger> ledgerEntries)
    {
        var matches = FindSingleMatches(bankStatementEntries, ledgerEntries).ToList();
        foreach (var match in matches)
            yield return match;

        bankStatementEntries.RemoveAll(x => matches.Select(x => x.BankStatementEntryId).Contains(x.Id));
        ledgerEntries.RemoveAll(x => matches.Select(x => x.GeneralLedgerId).Contains(x.GeneralLedgerId));
        var multiMatches = FindMultiMatches(bankStatementEntries, ledgerEntries);

         foreach (var match in multiMatches)
             yield return match;
    }

    private static IEnumerable<MatchResult> FindSingleMatches(List<EC_BankStatementEntry> bankStatementEntries, List<EC_GeneralLedger> ledgerEntries)
    {
        foreach (var bankStatementEntry in bankStatementEntries)
        {
            var ledgerMatch = ledgerEntries.SingleOrDefault(x => x.AmountLocalCurrency.EqualsApproximately(bankStatementEntry.Amount));
            if (ledgerMatch != null)
                yield return new MatchResult(bankStatementEntry.Id, ledgerMatch.GeneralLedgerId);
        }
    }

    private static IEnumerable<MatchResult> FindMultiMatches(List<EC_BankStatementEntry> bankStatementEntries, List<EC_GeneralLedger> ledgerEntries)
    {
        var result = new List<MatchResult>();
        ledgerEntries = ledgerEntries.Where(x => x.AmountLocalCurrency.HasValue).ToList();
        
        foreach (var bankStatementEntry in bankStatementEntries)
        {
            var match = FindSums(bankStatementEntry.Amount, ledgerEntries.Select(x => (x.GeneralLedgerId, x.AmountLocalCurrency!.Value)).ToList());
            result.AddRange(match.Select(x => new MatchResult(bankStatementEntry.BankStatementId, x)));
        }

        bankStatementEntries.RemoveAll(x => result.Select(x => x.BankStatementEntryId).Contains(x.Id));
        ledgerEntries.RemoveAll(x => result.Select(x => x.GeneralLedgerId).Contains(x.GeneralLedgerId));
        
        foreach (var ledgerEntry in ledgerEntries)
        {
            var match = FindSums(ledgerEntry.AmountLocalCurrency!.Value, bankStatementEntries.Select(x => (x.Id, x.Amount)).ToList());
            result.AddRange(match.Select(x => new MatchResult(x, ledgerEntry.GeneralLedgerId)));
        }

        return result;
    }

    private static int[] FindSums(decimal targetSum, List<(int Id, decimal AmountLocalCurrency)> idsWithAmounts)
    {
        var maxNumberOfVariations = (int)Math.Round(Math.Pow(2, idsWithAmounts.Count()) - 1);
        for (var i = 0; i < maxNumberOfVariations; i++)
        {
            var filteredList = idsWithAmounts.Filter(i);
            if (filteredList.Sum(x => x.AmountLocalCurrency).EqualsApproximately(targetSum))
                return filteredList.Select(x => x.Id).ToArray();
        }

        return Array.Empty<int>();
    }
}

public class MatchResult
{
    public MatchResult(int bankStatementEntryId, int generalLedgerId, int? ruleId = null)
    {
        BankStatementEntryId = bankStatementEntryId;
        GeneralLedgerId = generalLedgerId;
        RuleId = ruleId;
    }
    
    public int BankStatementEntryId { get; }
    public int GeneralLedgerId { get; }
    public int? RuleId { get; }
}