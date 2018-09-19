using UnityEditor;
using UnityEngine;

namespace Noyau.Lab.Procedural.Editor
{
    [CustomPropertyDrawer(typeof(ConditionalHideAttribute))]
    public sealed class ConditionalHideDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return OnValidateCondition(property) ? EditorGUI.GetPropertyHeight(property, label) : 0F;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (OnValidateCondition(property))
                EditorGUI.PropertyField(position, property, label);
        }

        private bool OnValidateCondition(SerializedProperty property)
        {
            ConditionalHideAttribute _cond = attribute as ConditionalHideAttribute;

            string _propertyPath = property.propertyPath.Replace(property.name, _cond.propertyName);

            property = property.serializedObject.FindProperty(_propertyPath);

            if (property == null)
                return true;

            switch (property.propertyType)
            {
                case SerializedPropertyType.Boolean:
                    return property.boolValue == _cond.boolValue;
                case SerializedPropertyType.Integer:
                    return property.intValue == _cond.intValue;
                case SerializedPropertyType.Enum:
                    return property.enumValueIndex == _cond.intValue;
                default:
                    Debug.LogWarning($"{typeof(ConditionalHideAttribute).Name} doesn't work with properties of type {property.propertyType}");
                    return true;
            }
        }
    } // class: ConditionalHideDrawer
} // namespace