using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using On.Reconciliation.Core.Commands;
using On.Reconciliation.Core.Queries;
using On.Reconciliation.Core.Services;

namespace On.Reconciliation.Api.Controllers;

[Route("[controller]")]
public class ReconciliationController : Controller
{
    private readonly IMatchingService _matchingService;
    private readonly IReconciliationCommands _reconciliationCommands;
    private readonly IStatementQueries _statementQueries;
    private readonly IRuleService _ruleService;

    public ReconciliationController(IMatchingService matchingService, IReconciliationCommands reconciliationCommands, IStatementQueries statementQueries, IRuleService ruleService)
    {
        _matchingService = matchingService;
        _reconciliationCommands = reconciliationCommands;
        _statementQueries = statementQueries;
        _ruleService = ruleService;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        var matchCount = 0; 
        var rulesRun = 0;
        
        var matches = _matchingService.FindAllMatches().ToList();
        if (matches.Any())
        {
            _reconciliationCommands.InsertMatches(matches);
        }

        matchCount += matches.Count();
        
        // after matching, check each remaining unreconciliated entry against list of rules
        rulesRun += _ruleService.RunAllRules();

        return Ok($"{matchCount} overføringer avstemt mot bok og {rulesRun} rader bokført etter regler");
    }
}