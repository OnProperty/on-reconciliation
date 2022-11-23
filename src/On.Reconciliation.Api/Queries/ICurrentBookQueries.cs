namespace On.Reconciliation.Api.Queries;

public interface ICurrentBookQueries
{
    object? FindMatchingEntry(object camt53StatementEntry);
}

public class CurrentBookQueries : ICurrentBookQueries
{
    public object? FindMatchingEntry(object camt53StatementEntry)
    {
        throw new NotImplementedException();
    }
}