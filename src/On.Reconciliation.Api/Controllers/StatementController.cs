using Microsoft.AspNetCore.Mvc;
using On.Reconciliation.Core.Commands;
using On.Reconciliation.Core.Queries;
using On.Reconciliation.Models.ViewModels;

namespace On.Reconciliation.Api.Controllers;

[Route("[controller]")]
public class StatementController: ControllerBase
{
    private readonly IStatementQueries _statementQueries;
    private readonly IAccountingClientQueries _accountingClientQueries;

    public StatementController(IStatementQueries statementQueries, IAccountingClientQueries accountingClientQueries)
    {
        _statementQueries = statementQueries;
        _accountingClientQueries = accountingClientQueries;
    }
    
    //TODO: auth 
    //TODO: verify access to client
    [HttpGet]
    public IEnumerable<StatementViewModel> GetUnmatchedEntries([FromQuery]string bankAccount)
    {
        var statements = _statementQueries.GetAllUnmatchedEntries(bankAccount);
        return statements.Select(x => x.ToViewModel());
    }
}