using UnityEngine;

namespace Kurao
{
    public class PlayerMoveState : PlayerBaseState
    {
        private float _speed;

        #region Animation Hash

        private readonly int _animIDSpeed;
        private readonly int _animIDMotionSpeed;
        private float _animationBlend;

        #endregion

        public PlayerMoveState(PlayerController player, PlayerStateMachine stateMachine, InputHandler inputHandler,
            CharacterData characterData) : base(player, stateMachine, inputHandler, characterData)
        {
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
            _animIDSpeed = Animator.StringToHash("Speed");
        }

        public override void Enter()
        {
            base.Enter();
            _inputHandler.Dash += OnDash;
            _inputHandler.Attack += OnMeleeAttack;
        }

        public override void Update()
        {
            base.Update();
            var input = _inputHandler.MoveInput;
            var inputMagnitude = input.magnitude;

            var targetSpeed = CalculateTargetSpeed(input);
            var currentHorizontalSpeed = GetCurrentHorizontalSpeed();

            _speed = UpdateSpeed(currentHorizontalSpeed, targetSpeed, inputMagnitude);
            _animationBlend = UpdateAnimationBlend(targetSpeed);

            _player.MoveAndRotate(_speed, verticalVelocity);

            UpdateAnimator(_speed, inputMagnitude);
        }

        public override void Exit()
        {
            base.Exit();
            _inputHandler.Dash -= OnDash;
            _inputHandler.Attack -= OnMeleeAttack;
        }

        private float CalculateTargetSpeed(Vector2 input)
        {
            return input == Vector2.zero ? 0.0f : _characterData.movementSpeed;
        }

        private float GetCurrentHorizontalSpeed()
        {
            var horizontalVelocity = new Vector3(_player.PlayerVelocity.x, 0, _player.PlayerVelocity.z);
            return horizontalVelocity.magnitude;
        }

        private float UpdateSpeed(float currentSpeed, float targetSpeed, float inputMagnitude)
        {
            const float speedOffset = 0.1f;
            var newSpeed = _speed;

            if (currentSpeed < targetSpeed - speedOffset || currentSpeed > targetSpeed + speedOffset)
            {
                newSpeed = Mathf.Lerp(
                    currentSpeed,
                    targetSpeed * inputMagnitude,
                    Time.deltaTime * _characterData.speedChangeRate);
                newSpeed = Mathf.Round(newSpeed * 1000f) / 1000f;
            }
            else
            {
                newSpeed = targetSpeed;
            }

            if (targetSpeed == 0 && newSpeed < 0.01f)
            {
                newSpeed = 0f;
            }

            return newSpeed;
        }
        
        
        private void OnDash()
        {
            if(!_player.CanDash)return;
            _stateMachine.ChangeState(_player.DashState);
        }
        
        private void OnMeleeAttack()
        {
            _stateMachine.ChangeState(_player.MeleeAttackState);
        }


        private float UpdateAnimationBlend(float targetSpeed)
        {
            var blend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * _characterData.speedChangeRate);
            return blend < 0.01f ? 0f : blend;
        }

        private void UpdateAnimator(float speed, float inputMagnitude)
        {
            _player.Animator.SetFloat(_animIDSpeed, speed);
            _player.Animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
        }
    }
}
