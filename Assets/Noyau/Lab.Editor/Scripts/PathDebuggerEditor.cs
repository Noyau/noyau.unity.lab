using UnityEditor;

namespace Noyau.Lab.Editor
{
    using Editor = UnityEditor.Editor;

    [CustomEditor(typeof(PathDebugger))]
    public sealed class PathDebuggerEditor : Editor
    {
        private Editor m_pathEditor = null;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            serializedObject.Update();
            SerializedProperty _path = serializedObject.FindProperty("m_path");
            if (_path.objectReferenceValue != null)
            {
                if (_path.isExpanded = EditorGUILayout.InspectorTitlebar(_path.isExpanded, _path.objectReferenceValue))
                {
                    CreateCachedEditor(_path.objectReferenceValue, null, ref m_pathEditor);
                    m_pathEditor.DrawDefaultInspector();
                }
            }
        }
    } // class: PathDebuggerEditor
} // namespace