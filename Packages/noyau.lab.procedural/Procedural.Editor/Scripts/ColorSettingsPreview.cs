using UnityEditor;
using UnityEngine;

namespace Noyau.Lab.Procedural.Editor
{
    [CustomPreview(typeof(ColorSettings))]
    public sealed class ColorSettingsPreview : ObjectPreview
    {
        private SerializedProperty m_texture = null;

        private Texture2D texture
        {
            get
            {
                if (m_texture == null)
                    m_texture = new SerializedObject(target)
                        .FindProperty("gradient")
                        .FindPropertyRelative("m_texture");

                return m_texture?.objectReferenceValue as Texture2D;
            }
        }

        public override bool HasPreviewGUI()
        {
            return texture != null;
        }

        public override bool MoveNextTarget()
        {
            m_texture = null;
            return base.MoveNextTarget();
        }

        public override GUIContent GetPreviewTitle()
        {
            return new GUIContent(texture.name);
        }

        public override void OnPreviewGUI(Rect r, GUIStyle background)
        {
            if (texture != null)
                EditorGUI.DrawPreviewTexture(r, texture);
            else
                EditorGUI.LabelField(r, new GUIContent("Not baked"), EditorStyles.centeredGreyMiniLabel);
        }
    } // class: ColorSettingsPreview
} // namespace