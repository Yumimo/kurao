
using UnityEngine;
using Kurao.Tools;

namespace Kurao
{
    public class PlayerDashState : PlayerBaseState
    {
        public PlayerDashState(PlayerController player, PlayerStateMachine stateMachine, InputHandler inputHandler, CharacterData characterData) : base(player, stateMachine, inputHandler, characterData)
        {
            _animIdDash = Animator.StringToHash("Dash");
        }

        private readonly int _animIdDash;
        private Timer _dashTimer;
        private float _dashDuration;
        private readonly float _dashSpeed = 10f;
        private Vector3 _dashDirection;
        
        
        
        public override void Enter()
        {
            base.Enter();
            _dashDirection = _player.transform.forward.normalized;

            _player.Animator.SetBool(_animIdDash, true);

            _dashTimer = new Timer(_characterData.dashLength);
            _dashTimer.OnTimerCompleted += OnDashComplete;
            _dashTimer.Start();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            _player.Dash(_dashDirection * (_dashSpeed * Time.fixedDeltaTime));
            _dashTimer.Tick(Time.fixedDeltaTime);
        }

        public override void Exit()
        {
            base.Exit();
            _player.ResetDash();
            _dashTimer.OnTimerCompleted -= OnDashComplete;
        }

        private void OnDashComplete()
        {
            _stateMachine.ChangeState(_player.MoveState);
            _player.Animator.SetBool(_animIdDash, false);
        }
        
    }
}
