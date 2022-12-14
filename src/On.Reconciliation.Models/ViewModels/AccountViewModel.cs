namespace On.Reconciliation.Models.ViewModels;

public class AccountViewModel
{
    public int AccountId { get; set; }
    public int AccountingClientId { get; set; }
    public string? BankAccount { get; set; }
    public int NumberOfUnReconciliatedStatements { get; set; }
    public int NumberOfUnbalancedStatements { get; set; }
}