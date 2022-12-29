using Microsoft.AspNetCore.Mvc;
using On.Reconciliation.Core.Queries;
using On.Reconciliation.Models.ViewModels;

namespace On.Reconciliation.Api.Controllers;

public class OverviewController : Controller
{
    private readonly IAccountingClientQueries _accountingClientQueries;
    private readonly IStatementQueries _statementQueries;

    public OverviewController(IAccountingClientQueries accountingClientQueries, IStatementQueries statementQueries)
    {
        _accountingClientQueries = accountingClientQueries;
        _statementQueries = statementQueries;
    }
    
    [HttpGet]
    public IEnumerable<AccountOverviewViewModel> GetSummaries()
    {
        var result = new List<AccountOverviewViewModel>();
        var month = DateTime.Now.Month;
        var accountingClientWithBankAccounts = _accountingClientQueries
            .GetAccountingClientsForUser("")
            .ToDictionary(x => x, y => _accountingClientQueries.GetBankAccounts(y));;
        
        foreach (var accountingClient in accountingClientWithBankAccounts)
        {
            foreach (var bankAccount in accountingClient.Value)
            {
                var entries = _statementQueries.GetAllEntriesForMonth(bankAccount, month).ToList();
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