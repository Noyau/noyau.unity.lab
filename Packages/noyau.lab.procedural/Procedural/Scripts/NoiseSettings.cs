using UnityEngine;

namespace Noyau.Lab.Procedural
{
    [System.Serializable]
    public sealed class NoiseSettings : ISerializationCallbackReceiver
    {
        public float strength = .4F;
        public float roughness = 2F;
        public float baseRoughness = 1F;
        public float persistence = .5F;
        public float minValue = .5F;
        [Range(1, 8)] public int layerCount = 1;
        public Vector3 center = Vector3.zero;

        public void OnAfterDeserialize()
        {
            // TODO make common attributes for "Min/Max" values etc.
            strength = Mathf.Max(strength, 0F);
            roughness = Mathf.Max(roughness, 0F);
            baseRoughness = Mathf.Max(baseRoughness, 0F);
            minValue = Mathf.Max(minValue, 0F);
            persistence = Mathf.Max(persistence, 0F);
        }
        public void OnBeforeSerialize() { }
    } // class: NoiseSettings
} // namespace