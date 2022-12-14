namespace On.Reconciliation.Core.Extensions;

public static class DecimalExtensions
{
    public static bool EqualsApproximately(this decimal self, decimal other, decimal margin = 0.001m)
    {
        return Math.Abs(self - other) < margin;
    }

    public static bool EqualsApproximately(this decimal? self, decimal other, decimal margin = 0.001m)
    {
        return self != null && self.Value.EqualsApproximately(other, margin);
    }
}