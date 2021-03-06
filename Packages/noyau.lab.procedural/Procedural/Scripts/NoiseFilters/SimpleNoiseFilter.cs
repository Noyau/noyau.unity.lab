﻿using UnityEngine;

namespace Noyau.Lab.Procedural.NoiseFilters
{
    public sealed class SimpleNoiseFilter : NoiseFilter
    {
        public SimpleNoiseFilter(NoiseSettings settings) : base(settings)
        { }

        public override float Evaluate(Vector3 point)
        {
            float _noise = 0F;
            float _frequency = m_settings.baseRoughness;
            float _amplitude = 1F;
            for (int i = 0; i < m_settings.layerCount; ++i)
            {
                _noise += (m_noise.Evaluate(_frequency * point + m_settings.center) + 1F) * .5F * _amplitude;
                _frequency *= m_settings.roughness;
                _amplitude *= m_settings.persistence;
            }
            return Mathf.Max(0F, _noise - m_settings.minValue) * m_settings.strength;
        }
    } // class: SimpleNoiseFilter
} // namespace