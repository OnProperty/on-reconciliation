using On.Reconciliation.Models.Database;

namespace On.Reconciliation.Models.ViewModels;

public class RuleViewModel
{
    public string Name { get; set; }
    public int AccountId { get; set; }
    public string ContainsDescription { get; set; }
    public DateTime LastChanged { get; set; }
    public int? AccountingClientId { get; set; }
    public int CreatedBy { get; set; }
}

public static class RuleExtensions
{
    public static RuleViewModel ToViewModel(this EC_ReconciliationRules rule)
    {
        return new RuleViewModel()
        {
            Name = rule.RuleName,
            AccountId = rule.AccountNumber,
            AccountingClientId = rule.AccountingClientId,
            ContainsDescription = rule.ContainsDescription,
            CreatedBy = rule.CreatedBy,
            LastChanged = rule.LastChanged
        };
    }
}