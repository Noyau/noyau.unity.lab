using UnityEngine;

namespace Noyau.Lab.Procedural
{
    [DisallowMultipleComponent, SelectionBase]
    public sealed class Planet : MonoBehaviour
    {
        public const int FaceCount = 6;
        public static readonly Vector3[] FacesUps =
        {
            Vector3.up, Vector3.down,
            Vector3.right, Vector3.left,
            Vector3.forward, Vector3.back,
        };

        [SerializeField, Range(2, 256)] private int m_resolution = 8;
        [SerializeField] private Material m_material = null;

        [SerializeField, HideInInspector] private MeshFilter[] m_meshFilters = { };
        private TerrainFace[] m_terrainFaces = { };

        private void Reset()
        {

        }

        private void OnValidate()
        {
            Initialize();
            GenerateMesh();
        }

        public void Initialize()
        {
            if (m_meshFilters == null || m_meshFilters.Length != FaceCount)
                m_meshFilters = new MeshFilter[FaceCount];

            if (m_terrainFaces == null || m_terrainFaces.Length != FaceCount)
                m_terrainFaces = new TerrainFace[FaceCount];

            for (int i = 0; i < FaceCount; ++i)
            {
                if (m_meshFilters[i] == null)
                {
                    GameObject _go = new GameObject($"Face {i + 1}");
                    _go.transform.SetParent(transform, false);
                    _go.AddComponent<MeshRenderer>().sharedMaterial = m_material;
                    m_meshFilters[i] = _go.AddComponent<MeshFilter>();
                    m_meshFilters[i].sharedMesh = new Mesh();
                    m_meshFilters[i].sharedMesh.name = $"{typeof(TerrainFace).Name} {i + 1}";
                }

                m_terrainFaces[i] = new TerrainFace(
                    m_meshFilters[i].sharedMesh, m_resolution, FacesUps[i]);
            }
        }

        public void GenerateMesh()
        {
            for (int i = 0; i < m_terrainFaces.Length; ++i)
                m_terrainFaces[i].GenerateMesh();
        }
    } // class: Planet
} // namespace