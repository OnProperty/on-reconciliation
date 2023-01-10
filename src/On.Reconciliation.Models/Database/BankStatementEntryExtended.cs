using On.Reconciliation.Models.Database.Pure;

namespace On.Reconciliation.Models.Database;

public class BankStatementEntryExtended: EC_BankStatementEntry
{
    public string BankAccount { get; set; }
    public int AccountingClientId { get; set; }
}