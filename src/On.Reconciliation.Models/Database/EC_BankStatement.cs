using System.Diagnostics;

namespace On.Reconciliation.Models.Database;

public class EC_BankStatement
{
    public int Id { get; set; }
    public string BankAccount { get; set; }
    public decimal OpeningBalance { get; set; }
    public DateTime OpeningDateTime { get; set; }
    public decimal ClosingBalance { get; set; }
    public DateTime ClosingDateTime { get; set; }
    public string StatementId { get; set; }
}