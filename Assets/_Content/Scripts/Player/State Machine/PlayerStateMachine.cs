using UnityEngine;

namespace Kurao
{
    public class PlayerStateMachine
    {
        private IState _currentState;
        
        public void ChangeState(IState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        public void Update()
        {
            if(_currentState == null) return;
            _currentState?.Update();
        }

        public void FixedUpdate()
        {
            if(_currentState == null) return;
            _currentState?.FixedUpdate();
        }
    }
}
