namespace Zata.Extension.Reflection
{
    public static class TypeHelper
    {
        public static bool IsDefaultValue(object? obj)
        {
            if (obj == null)
                return true;

            return obj.Equals(GetDefaultValue(obj.GetType()));
        }

        public static T? GetDefaultValue<T>()
        {
            return default;
        }

        public static object? GetDefaultValue(Type type)
        {
            if (type.IsValueType)
                return Activator.CreateInstance(type);

            return null;
        }
    }
}
