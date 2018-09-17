using UnityEngine;

namespace Noyau.Lab.Procedural.NoiseFilters
{
    public sealed class RigidNoiseFilter : NoiseFilter
    {
        public RigidNoiseFilter(NoiseSettings settings) : base(settings)
        { }

        public override float Evaluate(Vector3 point)
        {
            float _value;
            float _noise = 0F;
            float _frequency = m_settings.baseRoughness;
            float _amplitude = 1F;
            float _weight = 1F;
            for (int i = 0; i < m_settings.layerCount; ++i)
            {
                _value = 1F - Mathf.Abs(m_noise.Evaluate(_frequency * point + m_settings.center));
                _value *= _value * _weight;
                _weight = Mathf.Clamp01(_value * m_settings.weightMultiplier);
                _noise += _value * _amplitude;
                _frequency *= m_settings.roughness;
                _amplitude *= m_settings.persistence;
            }
            return Mathf.Max(0F, _noise - m_settings.minValue) * m_settings.strength;
        }
    } // class: RigidNoiseFilter
} // namespace