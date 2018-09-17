using UnityEngine;

namespace Noyau.Lab.Procedural
{
    [CreateAssetMenu(menuName = "Noyau Lab/Procedural/Shape Settings")]
    public sealed class ShapeSettings : ScriptableObject, ISettings
    {
        [System.Serializable]
        public sealed class NoiseLayer
        {
            public bool enabled = true;
            public bool firstLayerAsMask = false;
            public NoiseSettings settings = new NoiseSettings();
        } // class: NoiseLayer

        public float radius = .5F;
        public NoiseLayer[] noiseLayers = { };

        private void OnValidate()
        {
            radius = Mathf.Max(radius, 0F);
        }
    } // class: ShapeSettings
} // namespace