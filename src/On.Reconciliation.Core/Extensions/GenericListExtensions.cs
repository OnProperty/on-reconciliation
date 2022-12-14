namespace On.Reconciliation.Core.Extensions;

public static class GenericListExtensions
{
    public static List<T> Filter<T>(this List<T> list, bool[] filter)
    {
        var result = new List<T>();
        for (var i = 0; i < filter.Length; i++)
        {
            if (filter[i])
                result.Add(list[i]);
        }

        return result;
    }

    public static List<T> Filter<T>(this List<T> list, int filter)
    {
        var boolFilter = filter.ToBooleanArray().Reverse().ToArray();
        
        return list.Filter(boolFilter);
    }
    
    static void Show(int i, string label = "")
    {
        Console.WriteLine($"{i,3} = 0b{Convert.ToString(i, 2).PadLeft(32, '0')} {label}");
    }
}