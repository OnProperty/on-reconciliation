namespace On.Reconciliation.Models.Database.Pure;

public class EC_Reconciliation
{
    public int Id { get; set; }
    public int GeneralLedgerId { get; set; }
    public int BankStatementEntryId { get; set; }
    public DateTime LastChanged { get; set; }
    public int? RuleId { get; set; }
}