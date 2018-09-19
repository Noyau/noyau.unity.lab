using UnityEditor;
using UnityEngine;

namespace Noyau.Lab.Procedural.Editor
{
    [CustomPropertyDrawer(typeof(IntervalAttribute))]
    [CustomPropertyDrawer(typeof(GreaterThanAttribute))]
    [CustomPropertyDrawer(typeof(LesserThanAttribute))]
    public sealed class IntervalAttributeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            IntervalAttribute _interval = attribute as IntervalAttribute;

            if (_interval.isSlider)
            {
                if (_interval.isInteger)
                    EditorGUI.IntSlider(position, property, _interval.intMin, _interval.intMax, label);
                else
                    EditorGUI.Slider(position, property, _interval.min, _interval.max, label);
            }
            else
            {
                using (EditorGUI.ChangeCheckScope check = new EditorGUI.ChangeCheckScope())
                {
                    EditorGUI.PropertyField(position, property, label);

                    if (check.changed)
                    {
                        if (_interval.isInteger)
                            property.intValue = Mathf.Clamp(property.intValue, _interval.intMin, _interval.intMax);
                        else
                            property.floatValue = Mathf.Clamp(property.floatValue, _interval.min, _interval.max);
                    }
                }
            }
        }
    } // class: IntervalAttributeDrawer
} // namespace