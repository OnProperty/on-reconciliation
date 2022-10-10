using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using Dapper;
using FluentAssertions;
using On.Reconciliation.Models.Attributes;
using On.Reconciliation.Models.Database;

namespace On.Reconciliation.Api.UnitTests;

public class HydrationTests
{
    public readonly SqlConnection _conn;
    
    public HydrationTests()
    {
        _conn = new(ConnectionHelper.GetConnectionString);
    }
    
    [Theory]
    [InlineData(typeof(EC_AccountingClient), "EC_AccountingClient")]
    public void Get_Model_From_Database_via_Dapper<T>(Type type, string table) where T : new()
    {
        var reader = _conn.ExecuteReader($"SELECT TOP 1 * FROM {table}");

        var modelMemberList = new List<string>();
        MemberInfo[] fields = type.GetMembers();
        foreach (var g in fields.Where(p => p.MemberType == MemberTypes.Property))
        {
            if (g.GetCustomAttributes().Any(x => x is SkipDatabaseCheck))
                continue;

            modelMemberList.Add(g.Name);
        }

        reader.Read();

        var readerList = new List<string>();
        for (int i = 0; i < reader.FieldCount; i++)
        {
            readerList.Add(reader.GetName(i).ToLowerInvariant());
        }

        var sb = new StringBuilder();

        foreach (var modelMember in modelMemberList)
        {
            if (!readerList.Contains(modelMember.ToLowerInvariant()))
                sb.AppendLine($"\nModel {type} contains member {modelMember} which is not found in columns in table {table}.");
        }

        sb.Length.Should().Be(0, sb.ToString());
    }
}