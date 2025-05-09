using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Kurao
{
    [CreateAssetMenu(fileName = "InputHandler", menuName = "Scriptable Objects/InputHandler")]
    public class InputHandler : ScriptableObject, InputSystem_Actions.IPlayerActions
    {
        public event UnityAction Crouch = delegate { };
        public event UnityAction Attack = delegate { };
        
        private InputSystem_Actions inputAction;

        public Vector2 MoveInput { get; private set; }
        
        private void OnEnable()
        {
            if (inputAction == null )
            {
                inputAction = new InputSystem_Actions();
                inputAction.Player.SetCallbacks(this);
            }
            inputAction.Enable();
        }

        private void OnDisable()
        {
            inputAction.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveInput = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context)
        {

        }

        public void OnAttack(InputAction.CallbackContext context)
        {

        }

        public void OnInteract(InputAction.CallbackContext context)
        {
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    Crouch?.Invoke();
                    break;
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            
        }

        public void OnPrevious(InputAction.CallbackContext context)
        {

        }

        public void OnNext(InputAction.CallbackContext context)
        {

        }

        public void OnSprint(InputAction.CallbackContext context)
        {

        }
    }
}
