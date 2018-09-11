using UnityEngine;

namespace Noyau.Lab
{
    public abstract class Path : ScriptableObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="t">Normalized time (from 0 (inclusive) to 1 (inclusive))</param>
        /// <returns></returns>
        public abstract Vector3 Evaluate(float t);
        public abstract Vector3 Evaluate(float t, out Vector3 normal);
        public abstract Vector3 Evaluate(float t, out Vector3 normal, out Vector3 tangent);

        public abstract void DrawGizmos();
    } // class: Path
} // namespace