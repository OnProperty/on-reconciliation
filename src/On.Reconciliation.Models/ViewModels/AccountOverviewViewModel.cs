using On.Reconciliation.Models.Database;

namespace On.Reconciliation.Models.ViewModels;

public class AccountOverviewViewModel
{
    public int AccountingClientId { get; set; }
    public string BankAccount { get; set; }
    public List<EntryWithStatus> Entries { get; set; }
    public int BookedByRuleCount => Entries.Count(x => x.RuleId != null);
    public int UnbookedCount => Entries.Count(x => x.GeneralLedgerId == null);
}