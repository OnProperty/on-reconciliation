using Microsoft.AspNetCore.Mvc;
using On.Reconciliation.Core.Queries;
using On.Reconciliation.Models.ViewModels;

namespace On.Reconciliation.Api.Controllers;

[Route("[controller]")]
public class RuleController : ControllerBase
{
    private readonly IRuleQueries _ruleQueries;

    public RuleController(IRuleQueries ruleQueries)
    {
        _ruleQueries = ruleQueries;
    }
    
    [HttpGet]
    public ActionResult<List<RuleViewModel>> Index([FromQuery]string bankAccount)
    {
        var rules = _ruleQueries.GetAllRulesForBankAccount(bankAccount);
        return rules.Select(x => x.ToViewModel()).ToList();
    }
}