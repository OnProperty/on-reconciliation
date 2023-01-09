using Microsoft.AspNetCore.Mvc;
using On.Reconciliation.Core.Queries;
using On.Reconciliation.Models.ViewModels;

namespace On.Reconciliation.Api.Controllers;

[Route("[controller]")]
public class OverviewController : ControllerBase
{
    private readonly IAccountingClientQueries _accountingClientQueries;
    private readonly IStatementQueries _statementQueries;

    public OverviewController(IAccountingClientQueries accountingClientQueries, IStatementQueries statementQueries)
    {
        _accountingClientQueries = accountingClientQueries;
        _statementQueries = statementQueries;
    }
    
    [HttpGet("Summaries/{year}/{month}")]
    public IEnumerable<AccountOverviewViewModel> GetSummaries(int year, int month)
    {
        var result = new List<AccountOverviewViewModel>();
        var accountingClientWithBankAccounts = _accountingClientQueries
            .GetAccountingClientsForUser("")
            .ToDictionary(x => x, y => _accountingClientQueries.GetBankAccounts(y));
        
        foreach (var accountingClient in accountingClientWithBankAccounts)
        {
            foreach (var bankAccount in accountingClient.Value)
            {
                var entries = _statementQueries.GetAllEntriesForMonth(bankAccount, year, month).ToList();
                result.Add(new AccountOverviewViewModel()
                {
                    BankAccount = bankAccount,
                    AccountingClientId = accountingClient.Key,
                    Entries = entries
                });
            }
        }

        return result;
    }}