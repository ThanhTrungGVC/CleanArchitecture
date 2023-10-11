using Zata.Extension;

namespace System
{
    public static class ZataTypeExtensions
    {
        public static string GetFullNameWithAssemblyName(this Type type)
        {
            return type.FullName + ", " + type.Assembly.GetName().Name;
        }

        //
        // Summary:
        //     Determines whether an instance of this type can be assigned to an instance of
        //     the TTarget. Internally uses System.Type.IsAssignableFrom(System.Type).
        //
        // Type parameters:
        //   TTarget:
        //     Target type
        public static bool IsAssignableTo<TTarget>(this Type type)
        {
            Check.NotNull(type, "type");
            return type.IsAssignableTo(typeof(TTarget));
        }

        //
        // Summary:
        //     Determines whether an instance of this type can be assigned to an instance of
        //     the targetType. Internally uses System.Type.IsAssignableFrom(System.Type) (as
        //     reverse).
        //
        // Parameters:
        //   type:
        //     this type
        //
        //   targetType:
        //     Target type
        public static bool IsAssignableTo(this Type type, Type targetType)
        {
            Check.NotNull(type, "type");
            Check.NotNull(targetType, "targetType");
            return targetType.IsAssignableFrom(type);
        }

        //
        // Summary:
        //     Gets all base classes of this type.
        //
        // Parameters:
        //   type:
        //     The type to get its base classes.
        //
        //   includeObject:
        //     True, to include the standard System.Object type in the returned array.
        public static Type[] GetBaseClasses(this Type type, bool includeObject = true)
        {
            Check.NotNull(type, "type");
            List<Type> list = new();
            AddTypeAndBaseTypesRecursively(list, type.BaseType, includeObject);
            return list.ToArray();
        }

        //
        // Summary:
        //     Gets all base classes of this type.
        //
        // Parameters:
        //   type:
        //     The type to get its base classes.
        //
        //   stoppingType:
        //     A type to stop going to the deeper base classes. This type will be be included
        //     in the returned array
        //
        //   includeObject:
        //     True, to include the standard System.Object type in the returned array.
        public static Type[] GetBaseClasses(this Type type, Type stoppingType, bool includeObject = true)
        {
            Check.NotNull(type, "type");
            List<Type> list = new();
            AddTypeAndBaseTypesRecursively(list, type.BaseType, includeObject, stoppingType);
            return list.ToArray();
        }

        private static void AddTypeAndBaseTypesRecursively(List<Type> types, Type? type, bool includeObject, Type? stoppingType = null)
        {
            if (!(type == null) && !(type == stoppingType) && (includeObject || !(type == typeof(object))))
            {
                AddTypeAndBaseTypesRecursively(types, type.BaseType, includeObject, stoppingType);
                types.Add(type);
            }
        }
    }
}
