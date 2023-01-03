﻿using System.Text.Json.Serialization;
using On.Reconciliation.Models.Database;
using On.Reconciliation.Models.Database.Pure;

namespace On.Reconciliation.Models.ViewModels;

public class StatementViewModel
{
    public DateTime? Date { get; set; }
    public string? Description { get; set; }
    public decimal? Amount { get; set; }
    public int? GeneralLedgerId { get; set; }
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
    
    public static StatementViewModel ToViewModel(this EntryWithStatus statementEntry)
    {
        return new StatementViewModel()
        {
            GeneralLedgerId = statementEntry.GeneralLedgerId,
            Amount = statementEntry.Amount,
            Date = statementEntry.Timestamp,
            Description = statementEntry.AdditionalInfo
        };
    }
}