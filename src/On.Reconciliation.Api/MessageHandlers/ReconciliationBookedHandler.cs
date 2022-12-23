using On.Reconciliation.Core.Commands;
using On.Reconciliation.Core.Queries;
using On.Reconciliation.Core.Services;
using OnProperty.Messaging.Internal.Reconciliation.Events;
using Thon.Hotels.FishBus;

namespace On.Reconciliation.Api.MessageHandlers;

public class ReconciliationBookedHandler: IHandleMessage<ReconciliationBooked>
{
    private readonly IRuleQueries _ruleQueries;
    private readonly IReconciliationCommands _reconciliationCommands;
    private readonly IRuleCommands _ruleCommands;
    private readonly IGeneralLedgerQueries _generalLedgerQueries;
    private readonly ILogger<ReconciliationBookedHandler> _logger;

    public ReconciliationBookedHandler(IRuleQueries ruleQueries, IReconciliationCommands reconciliationCommands, IRuleCommands ruleCommands, IGeneralLedgerQueries generalLedgerQueries, ILogger<ReconciliationBookedHandler> logger)
    {
        _ruleQueries = ruleQueries;
        _reconciliationCommands = reconciliationCommands;
        _ruleCommands = ruleCommands;
        _generalLedgerQueries = generalLedgerQueries;
        _logger = logger;
    }
    
    public Task<HandlerResult> Handle(ReconciliationBooked message)
    {
        // we could find the statement(s) matching this voucheridentifier, or we can just have the system just do an
        // entire matching run for all currently unmatched entries
        try
        {
            var ruleBooking = _ruleQueries.GetRuleBooking(message.VoucherIdentifier);
            var matches = _generalLedgerQueries
                .GetByVoucherIdentifier(message.VoucherIdentifier)
                .Select(x => new MatchResult(ruleBooking.BankStatementEntryId, x.GeneralLedgerId, ruleBooking.RuleId))
                .ToList();
            if (!matches.Any())
                throw new Exception($"Failed handling reconciliation booked: could not find general ledger entries for voucher identifier {message.VoucherIdentifier}");
            _reconciliationCommands.InsertMatches(matches);
            _ruleCommands.RemoveTemporaryRuleBookingDetails(message.VoucherIdentifier);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Automatch after reconciliation booked failed: {}", ex.Message);
            return Task.FromResult(HandlerResult.Failed());
        }

        return Task.FromResult(HandlerResult.Success());
    }
}