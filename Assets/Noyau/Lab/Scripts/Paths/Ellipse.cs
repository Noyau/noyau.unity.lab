using UnityEngine;

namespace Noyau.Lab.Paths
{
    [CreateAssetMenu(menuName = "Noyau/Lab/Ellipse")]
    public sealed class Ellipse : Path
    {
        private const float _TwoPI = 2F * Mathf.PI;

        [SerializeField, Tooltip("Major Axis")] private float m_a = 1F;
        [SerializeField, Tooltip("Minor Axis")] private float m_b = 1F;

        public float majorAxis => m_a;
        public float minorAxis => m_b;

        [Header("Gizmos")]
        [SerializeField, Range(0F, 1F)] private float m_debugPoint = .25F;
        [SerializeField, Range(2, 100)] private int m_resolution = 50;

        public override Vector3 Evaluate(float t)
        {
            float _t = t * _TwoPI;

            return new Vector3
            {
                x = majorAxis * Mathf.Cos(_t),
                z = minorAxis * Mathf.Sin(_t),
                y = 0F,
            };
        }
        public override Vector3 Evaluate(float t, out Vector3 normal)
        {
            float _t = t * _TwoPI;
            float _cos = Mathf.Cos(_t);
            float _sin = Mathf.Sin(_t);
            float _denom = Mathf.Sqrt(_sin * _sin * majorAxis * majorAxis + _cos * _cos * minorAxis * minorAxis);

            normal = new Vector3
            {
                x = minorAxis * _cos / _denom,
                z = majorAxis * _sin / _denom,
                y = 0F,
            };

            return Evaluate(t);
        }
        public override Vector3 Evaluate(float t, out Vector3 normal, out Vector3 tangent)
        {
            Vector3 _point = Evaluate(t, out normal);

            tangent = Vector3.Cross(normal, Vector3.up);

            return _point;
        }

        public void Foci(out Vector3 f1, out Vector3 f2)
        {
            float _mag = Mathf.Sqrt(majorAxis * majorAxis - minorAxis * minorAxis);
            f1 = new Vector3(-_mag, 0F, 0F);
            f2 = new Vector3(+_mag, 0F, 0F);
        }

        public override void DrawGizmos()
        {
            // Draw path
            Vector3 _zero = Evaluate(0F);
            Gizmos.color = Color.white;
            for (int i = 1; i <= m_resolution; ++i)
            {
                float _k0 = (i - 1) / (float)m_resolution;
                float _k1 = i / (float)m_resolution;
                Gizmos.DrawLine(Evaluate(_k0), Evaluate(_k1));
            }

            // Draw foci
            Gizmos.color = Color.cyan;
            Vector3 _f1, _f2; Foci(out _f1, out _f2);
            Gizmos.DrawSphere(_f1, .1F);
            Gizmos.DrawSphere(_f2, .1F);

            // Draw point
            Vector3 _p, _n, _t;
            _p = Evaluate(m_debugPoint, out _n, out _t);
            Gizmos.color = Color.grey;
            Gizmos.DrawLine(_f1, _p);
            Gizmos.DrawLine(_f2, _p);
            Gizmos.DrawSphere(_p, .1F);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(_p, _n);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(_p, _t);
        }
    } // class: Ellipse
} // namespace