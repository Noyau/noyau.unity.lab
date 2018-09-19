using UnityEngine;

namespace Noyau.Lab.Procedural
{
    public sealed class ColorGenerator : Generator<ColorSettings>
    {
        public void UpdateElevation(Threshold elevation)
        {
            if (m_settings != null)
            {
                m_settings.elevation.SetTargetValue(
                    m_settings.material, (Vector2)elevation);
            }
        }

        public void UpdateColors()
        {
            if (m_settings != null)
            {
                m_settings.gradient.SetTargetValue(
                    m_settings.material);
            }
        }
    } // class: ColorGenerator
} // namespace