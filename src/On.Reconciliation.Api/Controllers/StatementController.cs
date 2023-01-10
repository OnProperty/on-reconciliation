using Microsoft.AspNetCore.Mvc;
using On.Reconciliation.Core.Commands;
using On.Reconciliation.Core.Queries;
using On.Reconciliation.Core.Services;
using On.Reconciliation.Models.ViewModels;
using OnProperty.Messaging.Internal.Reconciliation.Commands;

namespace On.Reconciliation.Api.Controllers;

[Route("[controller]")]
public class StatementController: ControllerBase
{
    private readonly IStatementQueries _statementQueries;
    private readonly IAccountingClientQueries _accountingClientQueries;
    private readonly IBookingService _bookingService;

    public StatementController(IStatementQueries statementQueries, IAccountingClientQueries accountingClientQueries, IBookingService bookingService)
    {
        _statementQueries = statementQueries;
        _accountingClientQueries = accountingClientQueries;
        _bookingService = bookingService;
    }
    
    //TODO: auth 
    //TODO: verify access to client
    [HttpGet]
    public IEnumerable<StatementViewModel> GetUnmatchedEntries([FromQuery]string bankAccount)
    {
        var statements = _statementQueries.GetAllUnmatchedEntries(bankAccount);
        return statements.Select(x => x.ToViewModel());
    }

    [HttpGet("{bankAccount}/{year}/{month}")]
    public List<StatementViewModel> Get(string bankAccount, int year, int month)
    {
        var statements = _statementQueries.GetAllEntriesForMonth(bankAccount, year, month);
        return statements.Select(x => x.ToViewModel()).ToList();
    }

    [HttpPost("{bankStatementEntryId}/book/{accountNumber}")]
    public void Book(int bankStatementEntryId, ushort accountNumber)
    {
        var entry = _statementQueries.GetEntryById(bankStatementEntryId);
        var command = new BookReconciliationCommand()
        {
            Amount = entry.Amount,
            DateTime = entry.Timestamp,
            VoucherIdentifier = Guid.NewGuid(),
            AccountingClientId = entry.AccountingClientId,
            BankAccountNumber = entry.BankAccount,
            AccountNumber = accountNumber
        };
        _bookingService.BookReconciliation(command);
    }
}