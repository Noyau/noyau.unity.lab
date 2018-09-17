using UnityEngine;

namespace Noyau.Lab.Procedural
{
    public sealed class TerrainFace
    {
        private Mesh m_mesh;
        private int m_resolution;
        private Vector3 m_localUp;
        private Vector3 m_localRight;
        private Vector3 m_localForward;
        private ShapeGenerator m_shapeGenerator;
        private bool m_enabled;

        public bool enabled
        {
            get { return m_enabled; }
            set { m_enabled = value; }
        }

        public TerrainFace(ShapeGenerator shapeGenerator, Mesh mesh, int resolution, Vector3 localUp, bool enabled)
        {
            m_mesh = mesh;
            m_resolution = resolution;
            m_localUp = localUp;
            m_localRight = new Vector3(localUp.y, localUp.z, localUp.x);
            m_localForward = Vector3.Cross(localUp, m_localRight);
            m_shapeGenerator = shapeGenerator;
            m_enabled = enabled;
        }

        public void GenerateMesh()
        {
            if (!enabled) return;

            Vector3[] _vertices = new Vector3[m_resolution * m_resolution];

            int _triangleIndex = 0;
            int _triangleResolution = m_resolution - 1;
            int[] _triangles = new int[_triangleResolution * _triangleResolution * 6];

            for (int i = 0; i < _vertices.Length; ++i)
            {
                int _ix = (i % m_resolution);
                int _iy = (i / m_resolution);

                float _x = 2F * _ix / _triangleResolution - 1F;
                float _y = 2F * _iy / _triangleResolution - 1F;

                _vertices[i] = m_shapeGenerator.CalculatePoint(
                    Vector3.ClampMagnitude(m_localUp + _x * m_localRight + _y * m_localForward, .5F));

                if (_ix < _triangleResolution && _iy < _triangleResolution)
                {
                    _triangles[_triangleIndex++] = i;
                    _triangles[_triangleIndex++] = i + m_resolution + 1;
                    _triangles[_triangleIndex++] = i + m_resolution;

                    _triangles[_triangleIndex++] = i;
                    _triangles[_triangleIndex++] = i + 1;
                    _triangles[_triangleIndex++] = i + m_resolution + 1;
                }
            }

            m_mesh.Clear();
            m_mesh.vertices = _vertices;
            m_mesh.triangles = _triangles;
            m_mesh.RecalculateNormals();
        }
    } // class: TerrainFace
} // namespace