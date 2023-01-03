namespace On.Reconciliation.Web.Helpers;

public static class MonthHelper
{
    private static string[] _shortNames = new[]
    {
        "Jan",
        "Feb",
        "Mar",
        "Apr",
        "Mai",
        "Jun",
        "Jul",
        "Aug",
        "Sep",
        "Okt",
        "Nov",
        "Des",
    };
    
    private static string[] _fullNames = new[]
    {
        "Januar",
        "Februar",
        "Mars",
        "April",
        "Mai",
        "Juni",
        "Juli",
        "August",
        "September",
        "Oktober",
        "November",
        "Desember",
    };

    public static string ShortName(int monthNumber)
    {
        monthNumber = Clamp(monthNumber);
        return _shortNames[monthNumber - 1];
    }

    public static string FullName(int monthNumber)
    {
        monthNumber = Clamp(monthNumber);
        return _fullNames[monthNumber - 1];
    }

    private static int Clamp(int monthNumber)
    {
        while (monthNumber < 1)
            monthNumber += 12;

        monthNumber = monthNumber % 12;
        if (monthNumber == 0)
            return 12;
        return monthNumber;
    }
}