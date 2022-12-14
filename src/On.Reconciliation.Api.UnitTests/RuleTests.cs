using FluentAssertions;
using On.Reconciliation.Core.Extensions;
using On.Reconciliation.Models.Database;

namespace On.Reconciliation.Api.UnitTests;

public class RuleTests
{
    private readonly List<EC_ReconciliationRules> _rules;

    public RuleTests()
    {
        _rules = new List<EC_ReconciliationRules>()
        {
            new EC_ReconciliationRules() {}
        };
    }
    
    [Theory]
    [InlineData("Det er noe her og litt mer", 3)]
    [InlineData("Det er noe annet her og litt mer", 4)]
    [InlineData("Denne skal ikke matche", null)]
    public void Correectly_matches_with_contains(string additionalInfo, int? expectedRuleId)
    {
        var entry = new EC_BankStatementEntry()
        {
            AdditionalInfo = additionalInfo
        };
        var success = entry.TryMatchAnyRule(MockRules.List, out var rule);
        if (expectedRuleId.HasValue)
        {
            success.Should().BeTrue();
            rule.RuleId.Should().Be(expectedRuleId);
        }
        else
        {
            success.Should().BeFalse();
        }
    }

    [Theory]
    [InlineData("Det er noe her og litt mer", 3)]
    public void Prioritises_local_rules_before_global(string additionalInfo, int? expectedRuleId)
    {
        // should match ruleid 3 which has an accountingclientid, and not ruleid 1 which doesn't
        var entry = new EC_BankStatementEntry()
        {
            AdditionalInfo = additionalInfo
        };
        var success = entry.TryMatchAnyRule(MockRules.List, out var rule);
        if (expectedRuleId.HasValue)
        {
            success.Should().BeTrue();
            rule.RuleId.Should().Be(expectedRuleId);
        }
        else
        {
            success.Should().BeFalse();
        }
    }
}

public static class MockRules
{
    private static List<(int RuleId, string Contains, int? AccountingClientId)> _rules => new()
    {
        (1, "noe her", null),
        (2, "foo", null),
        (3, "noe her", 1),
        (4, "noe annet her", 2),
    };

    public static List<EC_ReconciliationRules> List => _rules.Select((x, index) => new EC_ReconciliationRules()
    {
        RuleId = x.RuleId,
        ContainsDescription = x.Contains,
        AccountingClientId = x.AccountingClientId
    }).ToList();
}