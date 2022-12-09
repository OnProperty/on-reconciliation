using System.Text.Json.Serialization;
using On.Reconciliation.Models.Database;

namespace On.Reconciliation.Models.ViewModels;

public class StatementViewModel
{
    public DateTime? Date { get; set; }
    public string? Description { get; set; }
    public double? Amount { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ReconciliationStatus Status { get; set; }
}

public static class StatementViewModelExtensions
{
    public static StatementViewModel ToViewModel(this EC_BankStatementEntry statementEntry)
    {
        return new StatementViewModel()
        {
            Amount = statementEntry.Amount,
            Date = statementEntry.Timestamp,
            Description = statementEntry.AdditionalInfo
        };
    }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ReconciliationStatus
{
    Unknown,
    Unmatched,
    IsInBook,
    BookedByRule
}