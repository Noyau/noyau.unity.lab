using UnityEngine;

namespace Noyau.Lab.Procedural
{
    [CreateAssetMenu(menuName = "Noyau Lab/Procedural/Color Settings")]
    public sealed class ColorSettings : ScriptableObject
    {
        [ColorUsage(false, true)] public Color color = Color.white;
    } // class: ColorSettings
} // namespace