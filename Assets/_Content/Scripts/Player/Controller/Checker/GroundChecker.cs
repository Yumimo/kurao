using UnityEngine;

namespace Kurao
{
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] private float m_radius;
        [SerializeField] private LayerMask m_groundLayer;

        public bool IsGrounded { get; private set; }
        private void Update()
        {
            IsGrounded = Physics.Raycast(transform.position, Vector3.down, m_radius, m_groundLayer);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = IsGrounded? Color.green : Color.red;
            Gizmos.DrawWireSphere(transform.position, m_radius);
        }
    }
}
