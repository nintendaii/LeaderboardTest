using UnityEditor;

namespace Module.Core.Utilities {
    [System.Serializable]
    public class FolderReference {
        public string GUID;
        public string GetPath {
            get {
                var result = string.Empty;
#if UNITY_EDITOR
                result = AssetDatabase.GUIDToAssetPath(GUID);
#endif
                return result;
            }
        }
    }
}