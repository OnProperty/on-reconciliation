namespace On.Reconciliation.Models.Database;

public class EC_GeneralLedger
{
    public int GeneralLedgerId { get; set; }
    public int VoucherNumber { get; set; }
    public int AccountId { get; set; }
    public int? AccountingPeriod { get; set; }
    public DateTime? VoucherDate { get; set; }
    public byte? VatCode { get; set; }
    public string? Notes { get; set; }
    public decimal? AmountLocalCurrency { get; set; }
    public int? DimensionDepartment { get; set; }
    public byte? PostingRule { get; set; }
    public DateTime GeneralLedgerDate { get; set; }
    public int? DimensionProject { get; set; }
    public decimal? VATamount { get; set; }
    public int? JournalId { get; set; }
    public int AccountingYear { get; set; }
    public bool? PostingType { get; set; }
    public bool? PostingPrinted { get; set; }
    public decimal? CurrencyAmount { get; set; }
    public decimal? VATDistribution { get; set; }
    public int AccountingClientId { get; set; }
    public string? PostingDescription { get; set; }
    public int? DimensionRealEstate { get; set; }
    public int? DimensionObject { get; set; }
    public int? DimensionProduct { get; set; }
    public int? DimensionAssignment { get; set; }
    public int? DimensionEmployee { get; set; }
    public int? DimensionFree01 { get; set; }
    public int? DimensionFree02 { get; set; }
    public int? DimensionFree03 { get; set; }
    public int? DimensionFree04 { get; set; }
    public int? AgentClientID { get; set; }
    public DateTime? LastChanged { get; set; }
    public int? LastUpdateUserId { get; set; }
    public int? ReversedJournalId { get; set; }
    public int? TaskId { get; set; }
    public int? LoanId { get; set; }
    public int? VoucherId { get; set; }
    public int? KONTO { get; set; }
    public string? docid { get; set; }
    public int? AccountingPeriodValue { get; set; }
    public int? SettlementId { get; set; }
    public int? DimensionCommercial { get; set; }
    public int? DimensionCommercialType { get; set; }
    public int? VoucherDocumentGroupId { get; set; }
    public string? BankFileMessage { get; set; }
    public int? Color { get; set; }
    public Guid? VoucherIdentifier { get; set; }
    public bool? IsTransferredToXLedger { get; set; }
    public bool? IsTransferredFromXLedger { get; set; }
    public string? XLedgerDbId { get; set; }
    public bool? IsTransferredToVisma { get; set; }
    
}