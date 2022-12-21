using On.Reconciliation.Core.Commands;
using On.Reconciliation.Core.Extensions;
using On.Reconciliation.Core.Queries;
using On.Reconciliation.Models.Database;
using OnProperty.Messaging.Internal.Reconciliation.Commands;

namespace On.Reconciliation.Core.Services;

public interface IRuleService
{
    public int RunAllRules();
    public int RunAllRulesForBankAccount(string bankAccount);
}

public class RuleService : IRuleService
{
    private readonly IStatementQueries _statementQueries;
    private readonly IRuleQueries _ruleQueries;
    private readonly IBookingService _bookingService;
    private readonly IAccountingClientQueries _accountingClientQueries;
    private readonly IRuleCommands _ruleCommands;
    private readonly IVatQueries _vatQueries;

    public RuleService(IStatementQueries statementQueries, IRuleQueries ruleQueries, IBookingService bookingService, IAccountingClientQueries accountingClientQueries, IRuleCommands ruleCommands, IVatQueries vatQueries)
    {
        _statementQueries = statementQueries;
        _ruleQueries = ruleQueries;
        _bookingService = bookingService;
        _accountingClientQueries = accountingClientQueries;
        _ruleCommands = ruleCommands;
        _vatQueries = vatQueries;
    }


    public int RunAllRules()
    {
        var ruleCount = 0;
        var bankAccounts = _statementQueries.GetAllBankAccounts();
        foreach (var bankAccount in bankAccounts)
        {
            ruleCount += RunAllRulesForBankAccount(bankAccount);
        }

        return ruleCount;
    }

    public int RunAllRulesForBankAccount(string bankAccount)
    {
        var statementEntries = _statementQueries.GetAllUnmatchedEntries(bankAccount);
        var rules = _ruleQueries.GetAllRulesForBankAccount(bankAccount);
        var ruleCount = 0;
        
        foreach (var entry in statementEntries)
        {
            if (entry.TryMatchAnyRule(rules, out var matchedRule))
            {
                var command = CreateBookingCommand(entry, matchedRule!, bankAccount);
                _bookingService.BookReconciliation(command);
                _ruleCommands.StoreTemporaryRuleBookingDetails(command.VoucherIdentifier, matchedRule!.RuleId, entry.Id);
                ruleCount++;
            }
        }

        return ruleCount;
    }

    private BookReconciliationCommand CreateBookingCommand(EC_BankStatementEntry entry, EC_ReconciliationRules rule, string bankAccount)
    {
        return new BookReconciliationCommand()
        {
            Amount = entry.Amount,
            BankAccountNumber = bankAccount,
            DateTime = entry.Timestamp,
            VoucherIdentifier = Guid.NewGuid(),
            AccountNumber = rule.AccountNumber,
            VatCode = _vatQueries.GetVatById(rule.VatCodeId),
            AccountingClientId = _accountingClientQueries.GetByBankAccount(bankAccount)
        };
    }
}