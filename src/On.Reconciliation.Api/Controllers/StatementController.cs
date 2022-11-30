using Microsoft.AspNetCore.Mvc;
using On.Reconciliation.Core.Queries;
using On.Reconciliation.Models.ViewModels;

namespace On.Reconciliation.Api.Controllers;

public class StatementController
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
    public IEnumerable<StatementViewModel> GetByAccountingClientId([FromQuery]string bankAccount)
    {
        var statements = _statementQueries.GetAllOpenStatements(bankAccount);
        return statements.Select(x => x.ToViewModel());
    }
}