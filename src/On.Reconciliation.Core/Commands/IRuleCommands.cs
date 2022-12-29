using System.Data;
using Dapper;
using On.Reconciliation.Models.Database;
using On.Reconciliation.Models.Database.Pure;

namespace On.Reconciliation.Core.Commands;

public interface IRuleCommands
{
    void RemoveTemporaryRuleBookingDetails(Guid voucherIdentifier);
    void StoreTemporaryRuleBookingDetails(Guid voucherIdentifier, int ruleId, int entryId);
}

public class RuleCommands: IRuleCommands
{
    private readonly IDbConnection _connection;

    public RuleCommands(IDbConnection connection)
    {
        _connection = connection;
    }

    public void RemoveTemporaryRuleBookingDetails(Guid voucherIdentifier)
    {
        var query = @"DELETE FROM EC_ReconciliationRuleBookings WHERE VoucherIdentifier = @voucherIdentifier";
        _connection.Query(query, new {voucherIdentifier});
    }
    
    public void StoreTemporaryRuleBookingDetails(Guid voucherIdentifier, int ruleId, int entryId)
    {
        var ruleDetails = new EC_ReconciliationRuleBookings()
        {
            VoucherIdentifier = voucherIdentifier,
            RuleId = ruleId,
            BankStatementEntryId = entryId
        };
        var query = @"INSERT INTO EC_ReconciliationRuleBookings(VoucherIdentifier, RuleId, BankStatementEntryId) VALUES(@voucherIdentifier, @ruleId, @bankStatementEntryId)";
        _connection.Query(query, ruleDetails);
    }

}