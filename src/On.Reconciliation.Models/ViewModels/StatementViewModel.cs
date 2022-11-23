namespace On.Reconciliation.Models.ViewModels;

public class StatementViewModel
{
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; }
}