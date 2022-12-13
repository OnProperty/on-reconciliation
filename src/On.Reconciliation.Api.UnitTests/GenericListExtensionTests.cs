using FluentAssertions;
using On.Reconciliation.Core.Extensions;

namespace On.Reconciliation.Api.UnitTests;

public class GenericListExtensionTests
{
    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    [InlineData(3, 1, 2)]
    [InlineData(4, 3)]
    [InlineData(5, 1, 3)]
    [InlineData(6, 2, 3)]
    [InlineData(7, 1, 2, 3)]
    [InlineData(8, 4)]
    public void Foo(int filter, params int[] expectedNumbers)
    {
        var list = Enumerable.Range(1, 20).ToList();
        list = list.Filter(filter).ToList();
        list.Count.Should().Be(expectedNumbers.Length);
        foreach (var expectedNumber in expectedNumbers)
        {
            list.Should().Contain(expectedNumber);
        }
    }
}