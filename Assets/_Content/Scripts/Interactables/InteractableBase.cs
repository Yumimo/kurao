using UnityEngine;

namespace Kurao
{
    public class InteractableBase : MonoBehaviour, IInteract
    {
        [SerializeField] private GameObject _interactNotification;
        
        public virtual void OnEnter()
        {
            Debug.Log($"Enter  {this.GetType().Name}");
        }

        public virtual void Interact()
        {
            Debug.Log($"Interact  {this.GetType().Name}");
        }

        public virtual void OnExit()
        {
            Debug.Log($"Exit  {this.GetType().Name}");
        }
    }
}
