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
    public static StatementViewModel ToViewModel(this (EC_BankStatementEntry? StatementEntry, EC_AccountCurrentBook? BookEntry) statementTuples)
    {
        return new StatementViewModel()
        {
            Amount = statementTuples.StatementEntry?.Amount ?? statementTuples.BookEntry?.AmountLocalCurrency,
            Date = statementTuples.StatementEntry?.Timestamp ?? statementTuples.BookEntry?.DueDate,
            Description = statementTuples.StatementEntry?.AdditionalInfo ?? statementTuples.BookEntry?.Notes,
            Status = statementTuples.StatementEntry != null ? ReconciliationStatus.IsInBook : ReconciliationStatus.Unmatched
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