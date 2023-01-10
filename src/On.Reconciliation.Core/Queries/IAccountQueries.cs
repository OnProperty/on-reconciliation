using On.Reconciliation.Models.Database;

namespace On.Reconciliation.Core.Queries;

public interface IAccountQueries
{
    public AccountNumberDto[] GetAccountNumbers();
}