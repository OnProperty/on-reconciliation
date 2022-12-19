using OnProperty.Messaging.Internal.Reconciliation.Events;
using Thon.Hotels.FishBus;

namespace On.Reconciliation.Api.MessageHandlers;

public class ReconciliationBookingFailedHandler: IHandleMessage<ReconciliationBookingFailed>
{
    private readonly ILogger<ReconciliationBookingFailedHandler> _logger;

    public ReconciliationBookingFailedHandler(ILogger<ReconciliationBookingFailedHandler> logger)
    {
        _logger = logger;
    }
    
    public Task<HandlerResult> Handle(ReconciliationBookingFailed message)
    {
        _logger.LogError($"Booking with VoucherIdentifier {message.VoucherIdentifier} returned a {nameof(ReconciliationBookingFailed)} message with the following message: {message.ErrorMessage}");

        return Task.FromResult(HandlerResult.Success());
    }
}