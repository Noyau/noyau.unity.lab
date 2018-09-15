using UnityEngine;

namespace Noyau.Lab.Procedural
{
    public sealed class ShapeGenerator
    {
        private ShapeSettings m_settings;
        private NoiseFilter[] m_noiseFilters;

        public ShapeGenerator(ShapeSettings settings)
        {
            m_settings = settings;
            m_noiseFilters = new NoiseFilter[settings.noiseLayers.Length];
            for (int i = 0; i < m_noiseFilters.Length; ++i)
                m_noiseFilters[i] = new NoiseFilter(settings.noiseLayers[i].settings);
        }

        public Vector3 CalculatePoint(Vector3 pointOnUnitSphere)
        {
            // WARNING: our "unit sphere" is .5 radius (1 diameter)!
            pointOnUnitSphere *= 2F;

            float _radius = m_settings != null
                ? m_settings.radius
                : 1F;

            float _height = 0F;

            if (m_noiseFilters.Length > 0)
            {
                float _mask;
                float _baseHeight = m_noiseFilters[0].Evaluate(pointOnUnitSphere);

                if (m_settings.noiseLayers[0].enabled)
                    _height += _baseHeight;

                for (int i = 1; i < m_noiseFilters.Length; ++i)
                {
                    if (m_settings.noiseLayers[i].enabled)
                    {
                        _mask = m_settings.noiseLayers[i].firstLayerAsMask
                            ? _baseHeight
                            : 1F;

                        _height += _mask * m_noiseFilters[i].Evaluate(pointOnUnitSphere);
                    }
                }
            }

            return (_radius + _height) * pointOnUnitSphere;
        }
    } // class: ShapeGenerator
} // namespace