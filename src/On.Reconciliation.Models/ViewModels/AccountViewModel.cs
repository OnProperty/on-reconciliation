namespace On.Reconciliation.Models.ViewModels;

public class AccountViewModel
{
    public int AccountId { get; set; }
    public int AccountingClientId { get; set; }
    public string? BankAccount { get; set; }
    public List<StatementViewModel> Statements { get; set; }
    
}