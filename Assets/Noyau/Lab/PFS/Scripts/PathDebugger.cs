using UnityEngine;

namespace Noyau.Lab.PFS
{
    public sealed class PathDebugger : MonoBehaviour
    {
        [SerializeField] private Path m_path = null;

        private void OnDrawGizmos()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            m_path?.DrawGizmos();
        }
    } // class: PathDebugger
} // namespace