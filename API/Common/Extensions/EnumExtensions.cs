namespace Moneyon.PowerBi.API.Common.Extensions;

public static class EnumExtensions
{
    public static string GetValue<TEnum>(this TEnum enumValue)
    {
        foreach (TEnum item in Enum.GetValues(typeof(TEnum)))
        {
            if (item.Equals(enumValue))
            {
                return item.ToString();
            }
        }

        return string.Empty;
    }

    public static int GetId<TEnum>(this TEnum enumValue)
    {
        foreach (TEnum item in Enum.GetValues(typeof(TEnum)))
        {
            if (item.Equals(enumValue))
            {
                return Convert.ToInt32(item);
            }
        }

        return 0;
    }

}
