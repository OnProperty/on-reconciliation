namespace On.Reconciliation.Core.Queries;

public interface IAccountingClientQueries
{
    int[] GetAccountingClientsForUser(string userId);
    string[] GetBankAccounts(int accountingClientId);
}

public class AccountingClientQueries : IAccountingClientQueries
{
    public int[] GetAccountingClientsForUser(string userId)
    {
        throw new NotImplementedException();
    }

    public string[] GetBankAccounts(int accountingClientId)
    {
        throw new NotImplementedException();
    }
}