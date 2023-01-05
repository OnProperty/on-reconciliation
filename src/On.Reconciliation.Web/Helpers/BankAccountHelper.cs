namespace On.Reconciliation.Web.Helpers;

public static class BankAccountHelper
{
    public static string Dotted(string bankAccount)
    {
        if (string.IsNullOrEmpty(bankAccount))
            throw new ArgumentException("bankAccount must have a value");

        if (bankAccount.Length != 11)
            throw new ArgumentException("bankAccount must have 11 digits");
        
        return $"{bankAccount[0..4]}.{bankAccount[5..7]}.{bankAccount[6..]}";
}
}