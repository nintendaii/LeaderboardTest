using UnityEditor;

namespace Module.Core.Utilities {
    internal static class PropertyUtilityEditor {
        public static void InitSerializedObject(SerializedObject editorSerializedObject, out SerializedProperty self, string propertyPath) {
            self = editorSerializedObject?.FindProperty(propertyPath);
        }

        public static void InitRelativeProperty(SerializedProperty parentSerializedProperty, out SerializedProperty self, string relativePropertyPath) {
            self = parentSerializedProperty?.FindPropertyRelative(relativePropertyPath);
        }

        public static  void InitPropertyPathToExcludeForChildClasses(out string[] self, params SerializedProperty[] properties) {
            self = new string[properties.Length];
            for (var i = 0; i < properties.Length; i++) {
                self[i] = properties[i].propertyPath;
            }
        }
    }
}