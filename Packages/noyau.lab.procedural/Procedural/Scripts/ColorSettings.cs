using UnityEngine;

namespace Noyau.Lab.Procedural
{
    [CreateAssetMenu(menuName = "Noyau Lab/Procedural/Color Settings")]
    public sealed class ColorSettings : ScriptableObject, ISettings
    {
        [Header("Shared Material")]
        public Material material = null;

        [Header("Material Properties")]
        public GradientParam gradient = new GradientParam("_Gradient");
        public ShaderParam elevation = new ShaderParam("_Elevation");
    } // class: ColorSettings
} // namespace