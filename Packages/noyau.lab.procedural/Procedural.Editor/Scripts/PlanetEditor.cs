using UnityEditor;
using UnityEngine;

namespace Noyau.Lab.Procedural.Editor
{
    using Editor = UnityEditor.Editor;

    [CustomEditor(typeof(Planet))]
    public sealed class PlanetEditor : Editor
    {
        [MenuItem("GameObject/Noyau Lab/Procedural/Planet")]
        public static void CreatePlanet(MenuCommand cmd)
        {
            new GameObject("Planet (Procedural)").AddComponent<Planet>();
        }
    } // class: PlanetEditor
} // namespace