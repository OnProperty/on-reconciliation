namespace On.Reconciliation.Models.Database.Pure;

public class EC_ReconciliationRuleBookings
{
    public Guid VoucherIdentifier { get; set; }
    public int RuleId { get; set; }
    public int BankStatementEntryId { get; set; }
}