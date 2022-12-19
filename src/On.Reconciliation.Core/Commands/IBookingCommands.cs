using System.Data;
using Microsoft.Extensions.Configuration;
using On.Reconciliation.Models.Database;

namespace On.Reconciliation.Core.Commands;

public interface IBookingCommands
{
    public void BookStatementByRule(EC_BankStatementEntry bankStatementEntry, EC_ReconciliationRules ecReconciliationRules);
}

public class BookingCommands : IBookingCommands
{
    private readonly IDbConnection _connection;
    private readonly string _serviceBusConnectionString;

    public BookingCommands(IDbConnection connection, IConfiguration configuration)
    {
        _connection = connection;
        _serviceBusConnectionString = configuration.GetConnectionString("ReconciliationCommandServiceBus");
    }

    public void BookStatementByRule(EC_BankStatementEntry bankStatementEntry, EC_ReconciliationRules ecReconciliationRules)
    {
        // TODO: send message to booking service
        // TODO: add listener for responses, requires generating VoucherId (Guid) and storing it to relate it to the correct EntryId later
        throw new NotImplementedException();
    }
}