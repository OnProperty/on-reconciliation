namespace On.Reconciliation.Models.Database;

public class EC_Reconciliation
{
    public EC_BankStatement? BankStatement { get; set; }
    public int Id { get; set; }
    public int BankStatementId { get; set; }
    public int BookId { get; set; }
}