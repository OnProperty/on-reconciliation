namespace On.Reconciliation.Models.Attributes;

/// <summary>
/// Don't match property to database column name
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class SkipDatabaseCheck : Attribute
{
}