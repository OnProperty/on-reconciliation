namespace On.Reconciliation.Models.Database;

public class EC_BankStatementEntry
{
    public int Id { get; set; }
    public int BankStatementId { get; set; }
    public decimal Amount { get; set; }
    public string CurrencyCode { get; set; }
    public DateTime Timestamp { get; set; }
    public string EntryType { get; set; }
    public string AccountServiceReference { get; set; }
    public string? AdditionalInfo { get; set; }
    public string? AdditionalTransactionInfo { get; set; }
    public string? ProprietaryTransactionCode { get; set; }
    public string? ProprietaryTransactionIssuer { get; set; }
    public string? DomainCode { get; set; }
    public string? DomainFamily { get; set; }
    public string? DomainSubFamily { get; set; }
    public string? Reference { get; set; }
    public string? DebitorAccount { get; set; }
    public string? Cid { get; set; }
    public string? PaymentInformationIdentification { get; set; }
}