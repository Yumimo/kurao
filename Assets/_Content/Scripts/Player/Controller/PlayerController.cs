using System;
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
        public PlayerGroundState GroundedState { get; private set; }

        #endregion

        public Animator Animator => m_animator;
        public Vector3 PlayerVelocity => m_characterController.velocity;
        public bool IsGrounded => m_groundChecker.IsGrounded;
        
        
        private Camera m_camera;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;

        private void Awake()
        {
            InitializeStates();
            m_camera = Camera.main;
        }
        private void Update()
        {
            _stateMachine?.Update();
        }

        private void FixedUpdate()
        {
            _stateMachine?.FixedUpdate();
        }
        
        private void InitializeStates()
        {
            _stateMachine = new PlayerStateMachine();
            GroundedState = new PlayerGroundState(this, _stateMachine, m_input, m_data);
            
            _stateMachine.ChangeState(GroundedState);
        }
        
        public void MoveAndRotate(float speed, float _verticalVelocity)
        {
            var moveDirection = new Vector3(m_input.MoveInput.x, 0, m_input.MoveInput.y).normalized;

            if (m_input.MoveInput != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg +
                                  m_camera.transform.eulerAngles.y;
                var rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                    m_data.rotationSpeed);
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }
            var targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
            m_characterController.Move(targetDirection.normalized * (speed * Time.deltaTime) +
                             new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
        }
        
    }
}
