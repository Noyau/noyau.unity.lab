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

        [SerializeField] private ShapeSettings m_shapeSettings = null;
        [SerializeField] private ColorSettings m_colorSettings = null;

        private ShapeGenerator m_shapeGenerator = null;

        [SerializeField, Range(2, 256)] private int m_resolution = 8;
        [SerializeField] private bool m_autoUpdate = true;

        [SerializeField] private Material m_material = null;

        [SerializeField, HideInInspector] private MeshFilter[] m_meshFilters = { };
        [SerializeField, HideInInspector] private MeshRenderer[] m_meshRenderers = { };

        private TerrainFace[] m_terrainFaces = { };

        private void Reset()
        {

        }

        private void OnValidate()
        {
            Generate();
        }

        public void Initialize()
        {
            if (m_meshFilters == null || m_meshFilters.Length != FaceCount)
                m_meshFilters = new MeshFilter[FaceCount];

            if (m_meshRenderers == null || m_meshRenderers.Length != FaceCount)
                m_meshRenderers = new MeshRenderer[FaceCount];

            if (m_terrainFaces == null || m_terrainFaces.Length != FaceCount)
                m_terrainFaces = new TerrainFace[FaceCount];

            if (m_shapeGenerator == null)
                m_shapeGenerator = new ShapeGenerator(m_shapeSettings);

            for (int i = 0; i < FaceCount; ++i)
            {
                if (m_meshFilters[i] == null)
                {
                    GameObject _go = new GameObject($"Face {i + 1}");
                    _go.transform.SetParent(transform, false);
                    m_meshFilters[i] = _go.AddComponent<MeshFilter>();
                    m_meshFilters[i].sharedMesh = new Mesh();
                    m_meshFilters[i].sharedMesh.name = $"{typeof(TerrainFace).Name} {i + 1}";
                }

                if (m_meshRenderers[i] == null)
                    m_meshRenderers[i] = m_meshFilters[i].gameObject.AddComponent<MeshRenderer>();

                m_meshRenderers[i].sharedMaterial = m_material;

                m_terrainFaces[i] = new TerrainFace(m_shapeGenerator,
                    m_meshFilters[i].sharedMesh, m_resolution, FacesUps[i]);
            }
        }

        public void Generate()
        {
            Initialize();
            GenerateShapes();
            GenerateColors();
        }

        public void OnShapeSettingsUpdated()
        {
            if (m_autoUpdate)
            {
                Initialize();
                GenerateShapes();
            }
        }
        public void OnColorSettingsUpdated()
        {
            if (m_autoUpdate)
            {
                Initialize();
                GenerateColors();
            }
        }

        public void GenerateShapes()
        {
            for (int i = 0; i < m_terrainFaces.Length; ++i)
                m_terrainFaces[i].GenerateMesh();
        }
        public void GenerateColors()
        {
            if (m_colorSettings == null)
                return;

            for (int i = 0; i < m_meshRenderers.Length; ++i)
                m_meshRenderers[i].sharedMaterial.color = m_colorSettings.color;
        }
    } // class: Planet
} // namespace