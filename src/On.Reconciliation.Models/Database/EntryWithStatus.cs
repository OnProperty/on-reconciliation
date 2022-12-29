using On.Reconciliation.Models.Database.Pure;

namespace On.Reconciliation.Models.Database;

public class EntryWithStatus: EC_BankStatementEntry
{
    public int? GeneralLedgerId { get; set; }
    public int? RuleId { get; set; }
}