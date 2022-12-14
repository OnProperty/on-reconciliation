namespace On.Reconciliation.Models.Database;

public class EC_ReconciliationRules
{
    public int RuleId { get; set; }
    public int? AccountId { get; set; }
    public string ContainsDescription { get; set; }
    public int KonteringsFelt { get; set; }
    public bool IsGlobal { get; set; }
}