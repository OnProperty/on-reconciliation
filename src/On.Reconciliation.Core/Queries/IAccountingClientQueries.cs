namespace On.Reconciliation.Core.Queries;

public interface IAccountingClientQueries
{
    int[] GetAccountingClientsForUser(string userId);
    string[] GetBankAccounts(int accountingClientId);
}