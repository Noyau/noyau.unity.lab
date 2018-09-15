using UnityEngine;

namespace Noyau.Lab.Procedural
{
    [CreateAssetMenu(menuName = "Noyau Lab/Procedural/Shape Settings")]
    public sealed class ShapeSettings : ScriptableObject
    {
        public float radius = .5F;

        private void OnValidate()
        {
            radius = Mathf.Max(radius, 0F);
        }
    } // class: ShapeSettings
} // namespace