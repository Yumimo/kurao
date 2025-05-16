using UnityEngine;

namespace Kurao
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Player Components")]
        [SerializeField] private InputHandler m_input;
        [SerializeField] private CharacterController m_characterController;
        [SerializeField] private CharacterData m_data;
        
        [Header("Environment Checkers")]
        [SerializeField] private GroundChecker m_groundChecker;
        
        [Header("Animation & Visuals")]
        [SerializeField] private Animator m_animator;
        
        #region States

        private PlayerStateMachine _stateMachine;
        public PlayerMoveState MoveState { get; private set; }
        public PlayerMeleeAttackState MeleeAttackState  { get; private set; }
        public PlayerDashState DashState { get; private set; }

        #endregion

        public Animator Animator => m_animator;
        public Vector3 PlayerVelocity => m_characterController.velocity;
        public bool IsGrounded => m_groundChecker.IsGrounded;

        public bool CanDash => _dashDelayCounter > m_data.dashDelay;
        
        
        private Camera m_camera;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _dashDelayCounter;


        private void Awake()
        {
            InitializeStates();
            m_camera = Camera.main;
        }
        private void Update()
        {
            Counters();
            _stateMachine?.Update();
        }

        private void FixedUpdate()
        {
            _stateMachine?.FixedUpdate();
        }

        private void Counters()
        {
            _dashDelayCounter +=  Time.deltaTime;
        }

        private void InitializeStates()
        {
            _stateMachine = new PlayerStateMachine();
            MoveState = new PlayerMoveState(this, _stateMachine, m_input, m_data);
            MeleeAttackState = new PlayerMeleeAttackState(this, _stateMachine, m_input, m_data);
            DashState = new PlayerDashState(this, _stateMachine, m_input, m_data);
            
            _stateMachine.ChangeState(MoveState);
        }

        public void SetInteractable(IInteract interact)
        {
            
        }

        public void MoveAndRotate(float speed, float _verticalVelocity)
        {
            var inputDir = new Vector3(m_input.MoveInput.x, 0, m_input.MoveInput.y).normalized;

            if (inputDir == Vector3.zero)
                return;

            _targetRotation = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + m_camera.transform.eulerAngles.y;
            var rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, m_data.rotationSpeed);
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);

            var targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
            
            if (!IsGrounded)
            {
                var dot = Vector3.Dot(targetDirection.normalized, transform.forward);
                if (dot > 0.5f)
                {
                    targetDirection = Vector3.zero;
                }
            }

            m_characterController.Move(targetDirection.normalized * (speed * Time.deltaTime) +
                                       new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
        }

        #region Dash

        public void Dash(Vector3 _direction)
        {
            if(!IsGrounded)return;
            m_characterController.Move(_direction);
        }

        public void ResetDash()
        {
            _dashDelayCounter = 0;
        }
        
        #endregion
        
    }
}
