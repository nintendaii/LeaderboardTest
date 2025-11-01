using System;
using System.Linq;

namespace Module.Core.Utilities {
    public static class ExtensionAssembly {
        public static bool Contains<T>(this Type[] types) {
            return types.Contains(typeof(T));
        }

        public static bool Contains(this Type[] types, Type type) {
            return types != null && types.Length > 0 && types.Any(innerType => innerType == type);
        }

        public static bool ContainsInterface<T>(this Type type) {
            return type.ContainsInterface(typeof(T));
        }

        public static bool ContainsInterface(this Type type, Type interfaceType) {
            return type.GetInterfaces().Contains(interfaceType);
        }
    }
}