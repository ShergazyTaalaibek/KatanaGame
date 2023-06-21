using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    // gravity
    private float _gravity = -9.8f;
    private float _groundedGravity = -.05f;

    // jumpin
    private bool _isJumping = false;
    private float _initialJumpVelocity;
    [SerializeField, Range(1f, 5f)] private float _maxJumpHeight = 1.0f;
    [SerializeField, Range(.1f, 2f)] private float _maxJumpTime = .5f;

    // moving
    private bool _isMovementPressed = false;
    private PlayerInput _playerInput;
    private Vector2 _currentMovementInput;
    private Vector3 _currentMovement;
    private Vector3 _appliedMoveVelocity;
    [SerializeField] private float _movingSpeed = 2.0f;

    // dash
    private bool _isDashPressed = false;
    [SerializeField] private bool _canDash = false;
    [SerializeField, Range(5f, 100f)] private float _dashSpeed = 10f;
    [SerializeField, Range(0f, 1f)] private float _dashingTime = 1f;
    [SerializeField, Range(0f, 10f)] private float _dashCooldown = 1f;
    private float _dashCooldownTimer = 0;

    // others
    private CharacterController _controller;
    private Vector3 _playerVelocity;
    private Transform _cameraTransform;
    private Transform _playerTransform;

    // State Machine
    private BaseState _currentState;
    private StateFactory _states;


    // Animation
    private Animator _animator;

    // Getters & setters
    // Jump
    public bool IsJumping { get { return _isJumping; } }
    public float Gravity { get { return _gravity; } }
    public float GroundedGravity { get { return _groundedGravity; } }
    public float InitialJumpVelocity { get { return _initialJumpVelocity; } }
    public float PlayerJumpHeight { get { return _maxJumpHeight; } }
    public float PlayerVelocityY { get { return _playerVelocity.y; } set { _playerVelocity.y = value; } }
    // Move
    public float MoveingSpeed { get { return _movingSpeed; } }
    public bool IsMovementPressed { get { return _isMovementPressed; } }
    public float CurrentMovementX { get { return _currentMovement.x; } }
    public float CurrentMovementZ { get { return _currentMovement.z; } }
    public float CurrentMovementInputX { get { return _currentMovementInput.x; } }
    public float CurrentMovementInputY { get { return _currentMovementInput.y; } }
    public Vector3 AppliedMoveVelocity { get { return _appliedMoveVelocity; } set { _appliedMoveVelocity = value; } }
    // Dash
    public bool IsDashPressed { get { return _isDashPressed; } }
    public bool CanDash { get { return _canDash; } }
    public float DashSpeed { get { return _dashSpeed; } }
    public float DashingTime { get { return _dashingTime; } }
    public float DashCooldownTimer { set { _dashCooldownTimer = value; } }

    // Target
    public Transform CameraTransform { get { return _cameraTransform; } }
    public CharacterController Controller { get { return _controller; } }

    public Animator Animator { get { return _animator; } }
    public BaseState CurrentState { get { return _currentState; } set { _currentState = value; } }

    public void Initialize()
    {
        SetupJumpVariables();

        _controller = GetComponent<CharacterController>();
        _cameraTransform = Camera.main.transform;
        _playerTransform = GetComponent<Transform>();
        _playerVelocity = new Vector3(0, _gravity, 0);
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
        _currentState.UpdateStates();
        ApplyVelocityY();
        SetupJumpVariables(); // <-- must be disabled/commented
        CoolDownDash();
    }

    void CoolDownDash()
    {
        _dashCooldownTimer += Time.deltaTime;
        if (_dashCooldownTimer >= _dashCooldown)
            _canDash = true;
        else
            _canDash = false;
    }

    void SetupJumpVariables()
    {
        float timeToApex = _maxJumpTime / 2;
        _gravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        _initialJumpVelocity = (2 * _maxJumpHeight) / timeToApex;
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
    private void OnDashInput(InputAction.CallbackContext context) => _isDashPressed = context.ReadValueAsButton();

    private void OnEnable() => _playerInput.PlayerControlls.Enable();
    private void OnDisable() => _playerInput.PlayerControlls.Disable();
}
