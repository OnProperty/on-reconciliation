using Microsoft.Extensions.Options;
using On.Reconciliation.Models.Options;
using OnProperty.Messaging.Internal.Reconciliation.Commands;
using Thon.Hotels.FishBus;

namespace On.Reconciliation.Core.Services;

public interface IBookingService
{
    public Task BookReconciliation(BookReconciliationCommand command);
}

public class BookingService : IBookingService
{
    private readonly BookingServiceBusSettings _settings;

    public BookingService(IOptions<BookingServiceBusSettings> options)
    {
        _settings = options.Value;
    }
    
    public async Task BookReconciliation(BookReconciliationCommand command)
    {
        var publisher = new MessagePublisher(_settings.ConnectionString);
        await publisher.SendAsync(command);
    }
}