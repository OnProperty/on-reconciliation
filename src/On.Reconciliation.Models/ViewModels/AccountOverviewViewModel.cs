using On.Reconciliation.Models.Database;

namespace On.Reconciliation.Models.ViewModels;

public class AccountOverviewViewModel
{
    public int AccountingClientId { get; set; }
    public string BankAccount { get; set; }
    public List<EntryWithStatus> Entries { get; set; }
}