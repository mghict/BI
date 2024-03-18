namespace Moneyon.PowerBi.Common.ObjectMapper
{
    //public static class ObjectMapperExtensions
    //{

    //}

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ObjectMapperAttribute : Attribute
    {
        public ObjectMapperAttribute()
        {
        }
    }
}
