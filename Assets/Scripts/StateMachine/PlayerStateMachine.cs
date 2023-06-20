using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 2.0f;
    [SerializeField] private float _jumpHeight = 1.0f;
    [SerializeField] private float _gravityValue = -9.81f;
    [SerializeField] private Transform _target;
    
    private CharacterController _controller;
    private Vector3 _playerVelocity;
    private bool _groundedPlayer;
    private Transform _cameraTransform;
    private Transform _playerTransform;

    // State Machine
    [Header("States")]
    private BaseState _currentState;
    private StateFactory _states;

    // Input system
    private PlayerInput _playerInput;
    private Vector2 _currentMovementInput;
    private Vector3 _currentMovement;
    private bool _isMovementPressed;
    private bool _isJumpPressed;
    private bool _isJumping;
    private bool _isDashing;

    // Animation
    private Animator _animator;

    // Getters & setters
    public float PlayerJumpHeight { get { return _jumpHeight; } }
    public float GravityValue { get { return _gravityValue; } }
    public BaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public Animator Animator { get { return _animator; } }
    public bool IsJumpPressed { get { return _isJumpPressed; } }
    public bool IsJumping { get { return _isJumping; } }
    public bool IsDashing { get { return _isDashing; } }
    public float PlayerVelocityY { get { return _playerVelocity.y; } set { _playerVelocity.y = value; } }

    public void Initialize()
    {
        _controller = GetComponent<CharacterController>();
        _cameraTransform = Camera.main.transform;
        _playerTransform = GetComponent<Transform>();

        _animator = GetComponent<Animator>();

        // Setup state
        _states = new StateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState(); 

        // Set the player input callbacks
        _playerInput = new PlayerInput();
        _playerInput.PlayerControlls.Move.started += OnMovementInput;
        _playerInput.PlayerControlls.Move.canceled += OnMovementInput;
        _playerInput.PlayerControlls.Move.performed += OnMovementInput;
        _playerInput.PlayerControlls.Jump.started += OnJumpInput;
        _playerInput.PlayerControlls.Jump.canceled += OnJumpInput;
        _playerInput.PlayerControlls.Dash.started += OnDashInput;
        _playerInput.PlayerControlls.Dash.canceled += OnDashInput;
    }

    private void Update()
    {
        _currentState.UpdateState();
    }

    public void ApplyVelocityY() => _controller.Move(_playerVelocity * Time.deltaTime);

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _currentMovement.x = _currentMovementInput.x;
        _currentMovement.z = _currentMovementInput.y;
        _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
    }
    private void OnJumpInput(InputAction.CallbackContext context) => _isJumping = context.ReadValueAsButton();
    private void OnDashInput(InputAction.CallbackContext context) => _isDashing = context.ReadValueAsButton();

    private void OnEnable() => _playerInput.PlayerControlls.Enable();
    private void OnDisable() => _playerInput.PlayerControlls.Disable();
}
