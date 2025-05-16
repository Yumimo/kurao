using System;
using UnityEngine;

namespace Kurao
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private LayerMask m_layerMask;
        [SerializeField] private Vector3 m_interactOffset;
        [SerializeField] private float m_interactRadius;
        [SerializeField] private PlayerController m_controller;

        private readonly Collider[] m_colliders = new Collider[3];
        private IInteract _currentInteract;

        private void Update()
        {
            int found = Physics.OverlapSphereNonAlloc(m_controller.transform.position + m_controller.transform.forward + m_interactOffset, 
                m_interactRadius, m_colliders, m_layerMask);

            IInteract newInteract = null;

            if (found > 0)
            {
                for (var i = 0; i < found; i++)
                {
                    if (!m_colliders[i].TryGetComponent(out IInteract interact)) continue;
                    newInteract = interact;
                    break;
                }
            }

            if (_currentInteract == newInteract) return;
            _currentInteract?.OnExit();

            if (newInteract != null)
            {
                OnEnterInteractable(newInteract);
            }
            else
            {
                _currentInteract = null;
                m_controller.SetInteractable(null);
            }
        }

        private void OnEnterInteractable(IInteract interact)
        {
            _currentInteract = interact;
            m_controller.SetInteractable(interact);
            interact.OnEnter();
        }
        
        private void OnDrawGizmosSelected()
        {
            if (m_controller == null) return;
            Gizmos.color = Color.cyan;
            var sphereCenter = m_controller.transform.position + m_controller.transform.forward + m_interactOffset;

            Gizmos.DrawWireSphere(sphereCenter, m_interactRadius);
        }
        
    }
}
