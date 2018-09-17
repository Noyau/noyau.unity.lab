using UnityEngine;

namespace Noyau.Lab.Procedural
{
    public sealed class ShapeGenerator
    {
        private ShapeSettings m_settings;
        private NoiseFilter[] m_noiseFilters;

        public Threshold elevation { get; } = new Threshold();

        public void UpdateSettings(ShapeSettings settings)
        {
            m_settings = settings;

            if (m_noiseFilters == null || m_noiseFilters.Length != settings.noiseLayers.Length)
                m_noiseFilters = new NoiseFilter[settings.noiseLayers.Length];

            for (int i = 0; i < m_noiseFilters.Length; ++i)
                m_noiseFilters[i] = NoiseFilterFactory.CreateNoiseFilter(settings.noiseLayers[i].settings);
        }

        public Vector3 CalculatePoint(Vector3 pointOnUnitSphere)
        {
            // WARNING: our "unit sphere" is .5 radius (1 diameter)!
            pointOnUnitSphere *= 2F;

            if (m_settings == null)
                return pointOnUnitSphere;

            float _radius = m_settings.radius;

            float _height = 0F;

            if (m_noiseFilters != null && m_noiseFilters.Length > 0)
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

            _height += _radius;

            elevation.AddValue(_height);

            return _height * pointOnUnitSphere;
        }
    } // class: ShapeGenerator
} // namespace