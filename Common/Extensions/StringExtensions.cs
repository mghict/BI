using System.Text;

namespace Moneyon.PowerBi.Common.Extensions;

public static class StringExtensions
{

    public static long ToLong(this string value)
    {
        try
        {
            return Convert.ToInt64(value, 0);
        }
        catch
        {
            return 0;
        }
    }

    public static int ToInt(this string value)
    {
        try
        {
            return Convert.ToInt32(value, 0);
        }
        catch
        {
            return 0;
        }
    }

    public static long GenerateDateTimeSecurityCode(this DateTime value)
    {
        Random random = new Random();
        int rndYears = random.Next(-100, -10);
        int rndMonth = random.Next(-11, -1);
        int rndDay = random.Next(-29, -1);

        value = value.AddYears(rndYears).AddMonths(rndMonth).AddDays(rndDay);

        StringBuilder sb = new StringBuilder();
        sb.Append(value.Millisecond.ToString("0000"));
        sb.Append(value.Year.ToString("0000"));
        sb.Append(value.Second.ToString("000"));
        sb.Append(value.Month.ToString("00"));
        sb.Append(value.Minute.ToString("00"));
        sb.Append(value.Day.ToString("00"));
        sb.Append(value.Hour.ToString("00"));

        return sb.ToString().ToLong();

    }
}
