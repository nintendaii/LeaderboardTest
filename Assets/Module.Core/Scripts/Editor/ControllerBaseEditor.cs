using UnityEditor;
using UnityEngine;

namespace Module.Core.Scripts.Editor {
    [CustomEditor(typeof(MVC.ControllerMonoBase),true)]
    public class ControllerBaseEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            var origFontStyle = EditorStyles.label.fontStyle;
            EditorStyles.label.fontStyle = FontStyle.BoldAndItalic;
            EditorGUILayout.LabelField("namespace", target.GetType().Namespace, EditorStyles.label);
            EditorGUILayout.Space();
            EditorStyles.label.fontStyle = origFontStyle;
            base.OnInspectorGUI();
            Debug.Log(origFontStyle);
        }
    }
}