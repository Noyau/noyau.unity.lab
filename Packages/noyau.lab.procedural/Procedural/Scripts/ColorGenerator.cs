using UnityEngine;

namespace Noyau.Lab.Procedural
{
    public sealed class ColorGenerator
    {
        public const int TextureResolution = 64; // expected texture size: 64x2
        public const int ColorBufferSize = TextureResolution << 1;

        private ColorSettings m_settings;

        private Texture2D m_gradientTexture; // TODO release texture sometimes (?)
        private Color[] m_gradientColors;

        // TODO implement custom "preview" for texture

        public void UpdateSettings(ColorSettings settings)
        {
            m_settings = settings;

            if (m_gradientTexture == null)
                m_gradientTexture = new Texture2D(TextureResolution, 2);

            if (m_gradientColors == null || m_gradientColors.Length != ColorBufferSize)
                m_gradientColors = new Color[ColorBufferSize];
        }

        public void UpdateElevation(Threshold elevation)
        {
            if (m_settings == null)
                return;

            m_settings.material.SetVector(ColorSettings._Elevation, elevation);
        }

        public void UpdateColors()
        {
            if (m_settings == null || m_gradientTexture == null)
                return;

            for (int i = 0; i < TextureResolution; ++i)
                m_gradientColors[i] = m_gradientColors[i + TextureResolution] = m_settings.gradient.Evaluate(i / (TextureResolution - 1F));

            m_gradientTexture.SetPixels(m_gradientColors);
            m_gradientTexture.Apply();

            m_settings.material.SetTexture(ColorSettings._Gradient, m_gradientTexture);
        }
    } // class: ColorGenerator
} // namespace