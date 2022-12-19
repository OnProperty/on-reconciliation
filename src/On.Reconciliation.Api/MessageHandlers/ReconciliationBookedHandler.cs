using On.Reconciliation.Core.Commands;
using On.Reconciliation.Core.Queries;
using On.Reconciliation.Core.Services;
using OnProperty.Messaging.Internal.Reconciliation.Events;
using Thon.Hotels.FishBus;

namespace On.Reconciliation.Api.MessageHandlers;

public class ReconciliationBookedHandler: IHandleMessage<ReconciliationBooked>
{
    private readonly IMatchingService _matchingService;
    private readonly IReconciliationCommands _reconciliationCommands;
    private readonly ILogger<ReconciliationBookedHandler> _logger;

    public ReconciliationBookedHandler(IMatchingService matchingService, IReconciliationCommands reconciliationCommands, ILogger<ReconciliationBookedHandler> logger)
    {
        _matchingService = matchingService;
        _reconciliationCommands = reconciliationCommands;
        _logger = logger;
    }
    
    public Task<HandlerResult> Handle(ReconciliationBooked message)
    {
        // we could find the statement(s) matching this voucheridentifier, or we can just have the system just do an
        // entire matching run for all currently unmatched entries
        try
        {
            var matches = _matchingService.FindAllMatches().ToList();
            _reconciliationCommands.InsertMatches(matches);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Automatch after reconciliation booked failed: {}", ex.Message);
            return Task.FromResult(HandlerResult.Failed());
        }

        return Task.FromResult(HandlerResult.Success());
    }
}