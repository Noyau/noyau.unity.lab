using UnityEngine;

namespace Noyau.Lab.Procedural
{
    [CreateAssetMenu(menuName = "Noyau Lab/Procedural/Color Settings")]
    public sealed class ColorSettings : ScriptableObject, ISettings
    {
        [System.Obsolete("Use \"gradient\" value instead")]
        [ColorUsage(false, true)] public Color color = Color.white;
        public Material material = null;
        public GradientParam gradient = new GradientParam("_Gradient");
        public ShaderParam elevation = new ShaderParam("_Elevation");
    } // class: ColorSettings
} // namespace