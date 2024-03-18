//using System.ComponentModel;
//using System.Reflection;
//using System.Text;

//namespace Moneyon.PowerBi.Common.Extensions;

//public static class EnumerableExtensions
//{

//    public static string GetDescription(this Enum en)
//    {
//        Type type = en.GetType();
//        MemberInfo[] memInfo = type.GetMember(en.ToString());
//        if (memInfo != null && memInfo.Length > 0)
//        {
//            object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
//            if (attrs != null && attrs.Length > 0)
//                return ((DescriptionAttribute)attrs[0]).Description;
//        }

//        return en.ToString();
//    }

//    public static string GetListValue(this IEnumerable<string>? dto)
//    {
//        if (dto == null)
//            return string.Empty;

//        StringBuilder str = new StringBuilder();
//        foreach (var item in dto)
//        {
//            str.Append($"{item} | ");
//        }

//        var len = str.ToString().Length;
//        return str.ToString().Substring(0, len - 2).Trim();
//    }
//}
