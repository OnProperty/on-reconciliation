using System.Data;
using On.Reconciliation.Models.Database;

namespace On.Reconciliation.Core.Commands;

public interface IBookingCommands
{
    public void BookStatementByRule(EC_BankStatementEntry bankStatementEntry, EC_ReconciliationRules ecReconciliationRules);
}

public class BookingCommands : IBookingCommands
{
    private readonly IDbConnection _connection;

    public BookingCommands(IDbConnection connection)
    {
        _connection = connection;
    }

    public void BookStatementByRule(EC_BankStatementEntry bankStatementEntry, EC_ReconciliationRules ecReconciliationRules)
    {
        // TODO: send message to booking service
        // TODO: add listener for responses, requires generating VoucherId (Guid) and storing it to relate it to the correct EntryId later
        throw new NotImplementedException();
    }
}