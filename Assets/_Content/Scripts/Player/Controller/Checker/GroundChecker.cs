using UnityEngine;

namespace Kurao
{
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] private float m_length = 0.3f;
        [SerializeField] private float m_radius = 0.25f;
        [SerializeField] private LayerMask m_groundLayer;

        public bool IsGrounded { get; private set; }

        private void Update()
        {
            var origin = transform.position + Vector3.up * 0.1f; 
            IsGrounded = Physics.SphereCast(origin, m_radius, Vector3.down, out _, m_length + 0.1f, m_groundLayer);
            Debug.Log($"Can Forward: {IsGrounded}");
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = IsGrounded ? Color.green : Color.red;
            Vector3 origin = transform.position + Vector3.up * 0.1f;
            Gizmos.DrawWireSphere(origin + Vector3.down * m_length, m_radius);
        }
    }
}
