using UnityEngine;

namespace Kurao
{
    public class PlayerMeleeAttackState : PlayerBaseState
    {
        public PlayerMeleeAttackState(PlayerController player, PlayerStateMachine stateMachine, InputHandler inputHandler, CharacterData characterData) : base(player, stateMachine, inputHandler, characterData)
        {
            _animIdAttack = Animator.StringToHash("Attack");
        }

        private readonly int _animIdAttack;
        

        public override void Enter()
        {
            base.Enter();
            _player.Animator.SetBool(_animIdAttack, true);
            _player.Animator.speed = _characterData.baseAttackSpeed;
        }
        
        public override void Update()
        {
            base.Update();

            var stateInfo = _player.Animator.GetCurrentAnimatorStateInfo(0);

            if (!stateInfo.IsName("Melee Attack")) return;
            if (stateInfo.normalizedTime >= 1f)
            {
                _player.Animator.SetBool(_animIdAttack, false);
                _stateMachine.ChangeState(_player.MoveState); 
            }
        }

        public override void Exit()
        {
            _player.Animator.speed = 1;
        }
    }
}
