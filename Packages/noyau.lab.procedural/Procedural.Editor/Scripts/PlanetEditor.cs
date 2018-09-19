using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.EditorGUI;

namespace Noyau.Lab.Procedural.Editor
{
    using Editor = UnityEditor.Editor;

    [CustomEditor(typeof(Planet))]
    public sealed class PlanetEditor : Editor
    {
        [MenuItem("GameObject/Noyau Lab/Procedural/Planet")]
        public static void CreatePlanet(MenuCommand cmd)
        {
            new GameObject("Planet (Procedural)").AddComponent<Planet>();
        }

        private Editor m_shapeSettingsEditor = null;
        private Editor m_colorSettingsEditor = null;

        private SerializedProperty m_shapeSettings = null;
        private SerializedProperty m_colorSettings = null;
        private SerializedProperty m_autoUpdate = null;

        private void OnEnable()
        {
            m_shapeSettings = serializedObject.FindProperty("m_shapeSettings");
            m_shapeSettings.isExpanded = m_shapeSettings.objectReferenceValue != null;

            m_colorSettings = serializedObject.FindProperty("m_colorSettings");
            m_colorSettings.isExpanded = m_colorSettings.objectReferenceValue != null;

            m_autoUpdate = serializedObject.FindProperty("m_autoUpdate");
        }

        public override void OnInspectorGUI()
        {
            Planet _planet = target as Planet;

            using (ChangeCheckScope check = new ChangeCheckScope())
            {
                DrawDefaultInspector();

                if (check.changed && m_autoUpdate.boolValue)
                    _planet.Generate();
            }

            if (GUILayout.Button("Generate"))
                _planet.Generate();

            DrawPropertyEditor(m_shapeSettings, ref m_shapeSettingsEditor, _planet.OnShapeSettingsUpdated);
            DrawPropertyEditor(m_colorSettings, ref m_colorSettingsEditor, _planet.OnColorSettingsUpdated);
        }

        private void DrawPropertyEditor(SerializedProperty property, ref Editor editor, UnityAction onChangeCheck)
        {
            if (property.objectReferenceValue != null &&
                (property.isExpanded = EditorGUILayout.InspectorTitlebar(property.isExpanded, property.objectReferenceValue)))
            {
                CreateCachedEditorWithContext(property.objectReferenceValue, target, null, ref editor);

                using (ChangeCheckScope check = new ChangeCheckScope())
                {
                    editor.OnInspectorGUI();

                    if (check.changed)
                        onChangeCheck?.Invoke();
                }
            }
        }
    } // class: PlanetEditor
} // namespace