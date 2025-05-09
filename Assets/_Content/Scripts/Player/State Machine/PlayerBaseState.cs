using UnityEngine;

namespace Kurao
{
    public class PlayerBaseState : IState
    {
        protected readonly PlayerController _player;
        protected readonly PlayerStateMachine _stateMachine;
        protected readonly InputHandler _inputHandler;
        protected readonly CharacterData _characterData;
        
        
        protected float verticalVelocity;
        public PlayerBaseState(PlayerController player, PlayerStateMachine stateMachine, InputHandler inputHandler, CharacterData characterData)
        {
            _player = player;
            _stateMachine = stateMachine;
            _inputHandler = inputHandler;
            _characterData = characterData;
        }
        public virtual void Enter()
        {
            Debug.Log($"Enter State: {this.GetType().Name})");
            InitializeAnimationHash();
        }

        public virtual void Exit()
        {

        }

        public virtual void FixedUpdate()
        {

        }

        public virtual void Update()
        {
            if (verticalVelocity < _characterData.terminalVelocity)
            {
                verticalVelocity += _characterData.gravity * Time.deltaTime;
            }
        }

        public virtual void InitializeAnimationHash()
        {
            
        }
    }
}
