using UnityEngine;

namespace Noyau.Lab.Procedural
{
    [DisallowMultipleComponent, SelectionBase]
    public sealed class Planet : MonoBehaviour
    {
        public const int FaceCount = 6;
        public static readonly string[] FacesNames =
        {
            "Top", "Bottom",
            "Right", "Left",
            "Front", "Back",
        };
        public static readonly Vector3[] FacesUps =
        {
            Vector3.up, Vector3.down,
            Vector3.right, Vector3.left,
            Vector3.forward, Vector3.back,
        };

        [SerializeField] private FaceRenderMask m_faceRenderMask = FaceRenderMask.All;
        [SerializeField] private ShapeSettings m_shapeSettings = null;
        [SerializeField] private ColorSettings m_colorSettings = null;

        private ShapeGenerator m_shapeGenerator = new ShapeGenerator();
        private ColorGenerator m_colorGenerator = new ColorGenerator();

        [SerializeField, Range(2, 256)] private int m_resolution = 8;
        [SerializeField] private bool m_autoUpdate = true;

        [SerializeField, HideInInspector] private MeshFilter[] m_meshFilters = { };
        [SerializeField, HideInInspector] private MeshRenderer[] m_meshRenderers = { };

        private TerrainFace[] m_terrainFaces = { };

        private void Reset()
        {

        }

        private void OnValidate()
        {

        }

        public void Initialize()
        {
            if (m_meshFilters == null || m_meshFilters.Length != FaceCount)
                m_meshFilters = new MeshFilter[FaceCount];

            if (m_meshRenderers == null || m_meshRenderers.Length != FaceCount)
                m_meshRenderers = new MeshRenderer[FaceCount];

            if (m_terrainFaces == null || m_terrainFaces.Length != FaceCount)
                m_terrainFaces = new TerrainFace[FaceCount];

            m_shapeGenerator.UpdateSettings(m_shapeSettings);
            m_colorGenerator.UpdateSettings(m_colorSettings);

            FaceRenderMask _faceRenderMask;

            for (int i = 0; i < FaceCount; ++i)
            {
                if (m_meshFilters[i] == null)
                {
                    GameObject _go = new GameObject($"{FacesNames[i]} Face");
                    _go.transform.SetParent(transform, false);
                    m_meshFilters[i] = _go.AddComponent<MeshFilter>();
                    m_meshFilters[i].sharedMesh = new Mesh();
                    m_meshFilters[i].sharedMesh.name = $"{typeof(TerrainFace).Name} {i + 1}";
                }

                if (m_meshRenderers[i] == null)
                    m_meshRenderers[i] = m_meshFilters[i].gameObject.AddComponent<MeshRenderer>();

                m_meshRenderers[i].sharedMaterial = m_colorSettings.material;

                _faceRenderMask = (FaceRenderMask)(1 << i);

                m_terrainFaces[i] = new TerrainFace(m_shapeGenerator,
                    m_meshFilters[i].sharedMesh, m_resolution, FacesUps[i],
                    (_faceRenderMask & m_faceRenderMask) == _faceRenderMask);

                m_meshFilters[i].gameObject.SetActive(m_terrainFaces[i].enabled);
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
            {
                if (m_terrainFaces[i].enabled) // Note: yeah I know, there is a double check, I'm kinda paranoid ( @ _ @')
                    m_terrainFaces[i].GenerateMesh();
            }

            m_colorGenerator.UpdateElevation(m_shapeGenerator.elevation);
        }
        public void GenerateColors()
        {
            //if (m_colorSettings == null)
            //    return;

            //for (int i = 0; i < m_meshRenderers.Length; ++i)
            //    m_meshRenderers[i].sharedMaterial.color = m_colorSettings.color;

            m_colorGenerator.UpdateColors();
        }
    } // class: Planet
} // namespace