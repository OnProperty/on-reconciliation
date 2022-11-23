using Microsoft.AspNetCore.Mvc;
using On.Reconciliation.Api.Queries;
using On.Reconciliation.Models.ViewModels;

namespace On.Reconciliation.Api.Controllers;

public class StatementController
{
    private readonly IStatementQueries _statementQueries;

    public StatementController(IStatementQueries statementQueries)
    {
        _statementQueries = statementQueries;
    }
    
    //TODO: auth 
    //TODO: verify access to client
    [HttpGet]
    public async Task<StatementViewModel> GetByAccountingClientId()
    {
        throw new NotImplementedException();
    }
}