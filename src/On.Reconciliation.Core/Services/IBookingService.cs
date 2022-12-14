using Microsoft.Extensions.Configuration;
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
    private readonly string _serviceBusConnectionString;

    public BookingService(IConfiguration configuration)
    {
        _serviceBusConnectionString = configuration.GetConnectionString("BookingServiceBus");
    }
    
    public async Task BookReconciliation(BookReconciliationCommand command)
    {
        var publisher = new MessagePublisher(_serviceBusConnectionString);
        await publisher.SendAsync(command);
    }
}