using System;
using System.Collections.Generic;
using System.Linq;

namespace Module.Core.Utilities {
    public partial class Helper {
        public static class Assembly {
            private static readonly Type[] AllTypes = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();

            public static IEnumerable<Type> GetTypeListWithInterface<T>(bool inclusiveAbstract) {
                return AllTypes.Where(innerType => inclusiveAbstract || !innerType.IsAbstract).Where(type => type.GetInterfaces().Contains<T>()).ToList();
            }

            public static IEnumerable<Type> GetSubclassListThroughHierarchy<T>(bool inclusiveAbstract) {
                return GetSubclassListThroughHierarchy(typeof(T), inclusiveAbstract);
            }

            public static IEnumerable<Type> GetSubclassListThroughHierarchy(Type type, bool inclusiveAbstract) {
                if (type == null) {
                    throw new Exception("Type is null");
                }

                return AllTypes.Where(innerType => (inclusiveAbstract || !innerType.IsAbstract) && IsSubclass(type, innerType)).ToList();
            }

            public static bool IsSubclass(Type baseType, Type subclassType) {
                if (baseType == null) {
                    throw new ArgumentNullException(nameof(baseType));
                }

                if (subclassType == null) {
                    throw new ArgumentNullException(nameof(subclassType));
                }

                var typeToCheck = subclassType.BaseType;
                while (typeToCheck != null) {
                    if (typeToCheck == baseType) {
                        return true;
                    }

                    typeToCheck = typeToCheck.BaseType;
                }

                return false;
            }
        }
    }
}