using UnityEngine;

namespace Kurao
{
    public interface IInteract
    {
        public void OnEnter();
        public void Interact();
        public void OnExit();
    }
}
