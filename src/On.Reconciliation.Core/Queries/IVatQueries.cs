using System.Data;
using Dapper;

namespace On.Reconciliation.Core.Queries;

public interface IVatQueries
{
    byte? GetVatById(int? vatCodeId);
}

public class VatQueries : IVatQueries
{
    private readonly IDbConnection _connection;

    public VatQueries(IDbConnection connection)
    {
        _connection = connection;
    }
    
    public byte? GetVatById(int? vatCodeId)
    {
        if (!vatCodeId.HasValue)
            return null;
        
        var query = @"SELECT VatCode FROM EC_VatCodes WHERE VatCodeId = @vatCodeId";
        var code = _connection.QuerySingle<string>(query, new {vatCodeId});
        if (byte.TryParse(code, out byte vatCode))
        {
            return vatCode;
        }

        return null;
    }
}