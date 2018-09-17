using UnityEngine;

namespace Noyau.Lab.Procedural
{
    public abstract class NoiseFilter
    {
        protected NoiseSettings m_settings;
        protected Noise m_noise;

        public NoiseFilter(NoiseSettings settings)
        {
            m_settings = settings;
            m_noise = new Noise();
        }

        public abstract float Evaluate(Vector3 point);
    } // class: NoiseFilter
} // namespace