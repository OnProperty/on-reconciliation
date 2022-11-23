namespace On.Reconciliation.Api.Queries;

public interface IAccountingClientQueries
{
    int[] GetAccountingClientsForUser(string userId);
    string[] GetBankAccounts(int accountingClientId);
}