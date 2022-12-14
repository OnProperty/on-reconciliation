using System.Data;
using Dapper;
using On.Reconciliation.Models.Database;

namespace On.Reconciliation.Core.Queries;

public interface IRuleQueries
{
    List<EC_ReconciliationRules> GetAllRulesForBankAccount(string bankAccount, int? accountingClientId = null);
}

public class RuleQueries : IRuleQueries
{
    private readonly IDbConnection _connection;

    public RuleQueries(IDbConnection connection)
    {
        _connection = connection;
    }
    
    public List<EC_ReconciliationRules> GetAllRulesForBankAccount(string bankAccount, int? accountingClientId = null)
    {
        var query = @"SELECT rr.*
                        FROM EC_ReconciliationRules rr
                        LEFT JOIN EC_BankAccountRelations bar ON bar.AccountId = rr.AccountId
                        LEFT JOIN EC_BankAccount ba ON ba.BankAccountId = bar.BankAccountId
                        WHERE rr.AccountingClientId = @accountingClientId 
	                        OR ba.BankAccount = @bankAccount
";
        var result = _connection.Query<EC_ReconciliationRules>(query, new {bankAccount, accountingClientId});

        return result.ToList();
    }
}