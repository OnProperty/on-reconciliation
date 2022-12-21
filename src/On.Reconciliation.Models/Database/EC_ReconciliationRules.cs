namespace On.Reconciliation.Models.Database;

public class EC_ReconciliationRules
{
    public int RuleId { get; set; }
    public string RuleName { get; set; }
    public int? AccountingClientId { get; set; }
    public string ContainsDescription { get; set; }
    public int CreatedBy { get; set; }
    public DateTime LastChanged { get; set; }
    public string? PostingDescription { get; set; }
    public ushort AccountNumber { get; set; }
    public int? DimensionDepartmentId { get; set; }
    public int? DimensionProjectId { get; set; }
    public int? VatCodeId { get; set; }
}