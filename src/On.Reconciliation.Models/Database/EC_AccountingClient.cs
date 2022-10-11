using System.ComponentModel.DataAnnotations;

namespace On.Reconciliation.Models.Database;

public sealed record class EC_AccountingClient
{
    [Required]
    public int AccountingClientID { get; set; }

    [Required]
    [MaxLength(10)]
    public string AccountingClientNo { get; set; } = default!;

    [Required]
    public int ContactID { get; set; }

    public int AccountingClientType { get; set; }

    public int InCorporationNr { get; set; }

    public int AccountPlanID { get; set; }

    public int AccountingPeriodGroupID { get; set; }

    public int PostingRuleGroupID { get; set; }

    public int ManagerContactId { get; set; }

    public DateTime ExpiredDate { get; set; }

    public int LastUpdateUserId { get; set; }

    public DateTime LastChanged { get; set; }

    public string RecoveryRuleId { get; set; } = default!;

    public string DistributionRuleId { get; set; } = default!;

    public int AssociatedBBLId { get; set; }

    public bool UseBBLAccount { get; set; }

    public DateTime EstablishmentDate { get; set; }

    public bool IsFirstRefusalRight { get; set; }

    public string VATOptionId { get; set; } = default!;

    public string NumberOfStocks { get; set; } = default!;

    public string TotalShareCapital { get; set; } = default!;

    [MaxLength(50)]
    public string District { get; set; } = default!;

    public DateTime StartGeneralManagerDate { get; set; }

    public DateTime EndGeneralManagerDate { get; set; }

    public DateTime DateOfHandOver { get; set; }

    public int Jurisdiction { get; set; }

    [MaxLength(6)]
    public string ClientSeniority { get; set; } = default!;

    public int TotalAmountParkingSpaces { get; set; }

    public int TotalArea { get; set; }

    public int YearBuilt { get; set; }

    public string TotalDeposit { get; set; } = default!;

    public DateTime FirstMovingInDate { get; set; }

    public DateTime LastMovingInDate { get; set; }

    public int BBLBankAccountId { get; set; }

    public int Jurisdiction2 { get; set; }

    public int InterestRateId { get; set; }

    public int NumberOfApartments { get; set; }

    public bool UseBarCode { get; set; }

    public int TotalAmountGarages { get; set; }

    public bool UseCid { get; set; }

    public string Logo { get; set; } = default!;

    public bool UseClientAddress { get; set; }

    [Required]
    public string MinInterestAmountClient { get; set; } = default!;

    public string Status { get; set; } = default!;

    public int CommonVatClientId { get; set; }

    public int SelectedInvoiceLayout { get; set; }

    public bool ExternalAccounting { get; set; }

    public bool Factoring { get; set; }

    [MaxLength(128)]
    public string FactoringCreditorNumber { get; set; } = default!;

    public bool HasGuarantee { get; set; }

    public DateTime LastSyncedFactoring { get; set; }

    public bool FactoringFinancing { get; set; }

    public bool IsFactoringCompany { get; set; }

    public bool Efaktura { get; set; }

    public bool Tingsrettslig { get; set; }

    public DateTime ElmaStartDate { get; set; }

    public DateTime ElmaEndDate { get; set; }

    public DateTime InvoiceFlowStartDate { get; set; }

    public DateTime InvoiceFlowEndDate { get; set; }

    public bool DeviatingVoteDistribution { get; set; }

    public int ContractTypeId { get; set; }

    public DateTime EnergyMark { get; set; }

    //public bool AccountsSubjectToSubmission { get; set; }

    public bool OBOSKey { get; set; }

    [MaxLength(256)]
    public string IdentityGuid { get; set; } = default!;

    public int PartyInfoNumber { get; set; }

    public bool ContractsSent { get; set; }

    [MaxLength(256)]
    public string CreditorIdentityGuid { get; set; } = default!;

    public bool Preemption { get; set; }

    public bool ManagementApproval { get; set; }

    public int SecurityFundId { get; set; }

    public int InvoiceDueDayInMonth { get; set; }

    public int InvoiceRunDayInMonth { get; set; }

}