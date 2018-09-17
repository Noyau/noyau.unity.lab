using UnityEngine;

namespace Noyau.Lab.Procedural
{
    [CreateAssetMenu(menuName = "Noyau Lab/Procedural/Color Settings")]
    public sealed class ColorSettings : ScriptableObject
    {
        public static readonly int _Gradient = Shader.PropertyToID("_Gradient");
        public static readonly int _Elevation = Shader.PropertyToID("_Elevation");

        [System.Obsolete("Use \"gradient\" value instead")]
        [ColorUsage(false, true)] public Color color = Color.white;
        public Gradient gradient = new Gradient();
        public Material material = null;

        private void Reset()
        {
            gradient.mode = GradientMode.Blend;
            gradient.SetKeys(
                new[] { new GradientColorKey(Color.black, 0F), new GradientColorKey(Color.white, 1F), },
                new[] { new GradientAlphaKey(1F, 0F), new GradientAlphaKey(1F, 1F), });
        }
    } // class: ColorSettings
} // namespace