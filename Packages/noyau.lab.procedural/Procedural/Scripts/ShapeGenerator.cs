using UnityEngine;

namespace Noyau.Lab.Procedural
{
    public sealed class ShapeGenerator
    {
        private ShapeSettings m_settings;

        public ShapeGenerator(ShapeSettings settings)
        {
            m_settings = settings;
        }

        public Vector3 CalculatePoint(Vector3 pointOnUnitSphere)
        {
            // WARNING: our "unit sphere" is .5 radius (1 diameter)!
            if (m_settings == null)
                return pointOnUnitSphere;
            return m_settings.radius * 2F * pointOnUnitSphere;
        }
    } // class: ShapeGenerator
} // namespace