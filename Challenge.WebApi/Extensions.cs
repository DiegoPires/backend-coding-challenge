/// <summary>
/// Extensions util for the service
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Extension du convert to decimal
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static decimal ToDecimal(this object number)
    {
        decimal value;
        
        if (number == null) return 0;
        if (decimal.TryParse(number.ToString().Replace("$", "").Replace(",", ""), out value))
            return value;
        else
            return 0;
    }
}