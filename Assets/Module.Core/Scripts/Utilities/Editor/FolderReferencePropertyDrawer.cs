using System.IO;
using UnityEditor;
using UnityEngine;

namespace Module.Core.Utilities {
    [CustomPropertyDrawer(typeof(FolderReference))]
    public class FolderReferencePropertyDrawer : PropertyDrawer {
        private bool isInitialized;
        private SerializedProperty GUID;
        private Object objectReference;
 
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            if (!isInitialized) {
                InitProperties(property);
                InitData();
            }

            EditorGUI.BeginProperty(position, label, property);
            {
                var pathHeight = EditorGUI.GetPropertyHeight(GUID);
                var pathRect = new Rect(position.x, position.y, position.width, pathHeight);
                position.y += pathHeight;
                objectReference = (DefaultAsset) EditorGUI.ObjectField(pathRect, "Field Path", objectReference, typeof(DefaultAsset), false);
                var path = AssetDatabase.GetAssetPath(objectReference);
                if (Directory.Exists(path)) {
                    GUID.stringValue = AssetDatabase.AssetPathToGUID(path);
                }
            }
            EditorGUI.EndProperty();
        }
        
        private void InitProperties(SerializedProperty property) {
            PropertyUtilityEditor.InitRelativeProperty(property, out GUID, Helper.Member.GetName(() => GUID));
            isInitialized = true;
        }

        private void InitData() {
            objectReference = AssetDatabase.LoadAssetAtPath<Object>(AssetDatabase.GUIDToAssetPath(GUID.stringValue));
        }
    }
}