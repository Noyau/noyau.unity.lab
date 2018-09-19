using UnityEngine;

namespace Noyau.Lab.Procedural
{
    [System.Serializable]
    public sealed class NoiseSettings
    {
        public NoiseMethod noiseMethod = NoiseMethod.Simple;
        [GreaterThan(0F)] public float strength = .4F;
        [GreaterThan(0F)] public float roughness = 2F;
        [GreaterThan(0F)] public float baseRoughness = 1F;
        [GreaterThan(0F)] public float persistence = .5F;
        [GreaterThan(0F)] public float minValue = .5F;

        [ConditionalHide("noiseMethod", (int)NoiseMethod.Rigid)]
        [GreaterThan(0F)] public float weightMultiplier = .8F; // only works for "RigidNoiseFilters"

        [Range(1, 8)] public int layerCount = 1; // NOTE: I could use my *marvelous* IntervalAttribute but meh... It's shorter this way

        public Vector3 center = Vector3.zero;
    } // class: NoiseSettings
} // namespace