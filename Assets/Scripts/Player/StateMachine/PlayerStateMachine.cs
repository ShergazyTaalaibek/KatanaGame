using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    // gravity
    private float _gravity = -9.8f;
    private float _groundedGravity = -.05f;

    // stamina
    [SerializeField, Range(1, 10)] private float _maxStamina = 5;
    [SerializeField, Range(0, 10)] private float _staminaReducer = 1;
    [SerializeField] private float _currentStamina = 0;

    // jumping
    private bool _isJumpingPressed = false;
    private bool _isJumping = false;
    private float _initialJumpVelocity;
    [SerializeField, Range(1f, 5f)] private float _maxJumpHeight = 1.0f;
    [SerializeField, Range(.1f, 2f)] private float _maxJumpTime = .5f;

    // moving
    private bool _isMovementPressed = false;
    private bool _isRunningPressed = false;
    private bool _isRunning = false;
    private PlayerInput _playerInput;
    private Vector2 _currentMovementInput;
    private Vector3 _currentMovement;
    private Vector3 _appliedMoveVelocity;
    private Vector3 _playerVelocity;
    [SerializeField] private float _movingSpeed = 2.0f;
    [SerializeField] private float _runningSpeed = 4.0f;

    // dash
    private bool _isDashPressed = false;
    private bool _isDashing = false;
    [SerializeField] private bool _canDash = false;
    [SerializeField, Range(5f, 100f)] private float _dashSpeed = 10f;
    [SerializeField, Range(0f, 1f)] private float _dashingDuration = 1f;
    [SerializeField, Range(0f, 10f)] private float _dashCooldown = 1f;
    private float _dashCooldownTimer = 0;

    // attack
    private bool _isAttackPressed = false;
    private bool _isAttacking = false;
    [SerializeField] private bool _canAttack = false;
    [SerializeField, Range(0f, 1f)] private float _attackDuration = 1f;
    [SerializeField, Range(0f, 10f)] private float _attackCooldown = 1f;
    private float _attackCooldownTimer = 0;
    [SerializeField] private WeaponCollision _swordCollision;

    // death
    [SerializeField] private bool _isDead = false;

    // animation
    private HeadIKAnim _playerHeadIKAnim;

    // others
    private CharacterController _controller;
    private Transform _playerTransform;
    [SerializeField] private Transform _headTransform;
    [SerializeField] private Transform _lookAtTarget;
    [SerializeField] private float _rotatingSpeed = 2;
    [SerializeField] private float _modelRotatingSpeed = 1000;
    [SerializeField] private HeadIKAnim _headIKAnim;
    [SerializeField] private Transform _model;
    [SerializeField] private StaminaSlider _staminaSlider;

    // State Machine
    private PlayerBaseState _currentState;
    private PlayerStateFactory _states;

    // Getters & setters
    // Stamina
    public float CurrentStamina { get { return _currentStamina; } set { _currentStamina = value; } }
    public float StaminaReducer { get { return _staminaReducer; } }
    // Jump
    public bool IsJumpingPressed { get { return _isJumpingPressed; } }
    public bool IsJumping { get { return _isJumping; } set { _isJumping = value; } }
    public float Gravity { get { return _gravity; } }
    public float GroundedGravity { get { return _groundedGravity; } }
    public float InitialJumpVelocity { get { return _initialJumpVelocity; } }
    public float PlayerJumpHeight { get { return _maxJumpHeight; } }
    public float PlayerVelocityY { get { return _playerVelocity.y; } set { _playerVelocity.y = value; } }
    // Move
    public CharacterController Controller { get { return _controller; } }
    public float MovingSpeed { get { return _movingSpeed; } }
    public float RunningSpeed { get { return _runningSpeed; } }
    public bool IsMovementPressed { get { return _isMovementPressed; } }
    public bool IsRunningPressed { get { return _isRunningPressed; } }
    public bool IsRunning { get { return _isRunning; } set { _isRunning = value; } }
    public float CurrentMovementX { get { return _currentMovement.x; } }
    public float CurrentMovementZ { get { return _currentMovement.z; } }
    public float CurrentMovementInputX { get { return _currentMovementInput.x; } }
    public float CurrentMovementInputY { get { return _currentMovementInput.y; } }
    public Vector3 AppliedMoveVelocity { get { return _appliedMoveVelocity; } set { _appliedMoveVelocity = value; } }
    // Dash
    public bool IsDashPressed { get { return _isDashPressed; } }
    public bool IsDashing { get { return _isDashing; } set { _isDashing = value; } }
    public bool CanDash { get { return _canDash; } set { _canDash = value; } }
    public float DashSpeed { get { return _dashSpeed; } }
    public float DashingDuration { get { return _dashingDuration; } }
    public float DashCooldownTimer { set { _dashCooldownTimer = value; } }
    // Attack
    public bool IsAttackPressed { get { return _isAttackPressed; } }
    public bool IsAttacking { get { return _isAttacking; } set { _isAttacking = value; } }
    public bool CanAttack { get { return _canAttack; } set { _canAttack = value; } }
    public float AttackDuration { get { return _attackDuration; } }
    public float AttackCooldownTimer { set { _attackCooldownTimer = value; } }
    public WeaponCollision SwordCollision { get { return _swordCollision; } set { _swordCollision = value; } }
    // Death
    public bool IsDead { get { return _isDead; } set { _isDead = value; } }
    // Target
    public HeadIKAnim HeadIKAnim { get { return _headIKAnim; } }
    // State Machine
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public Transform PlayerTransform { get { return _playerTransform; } }

    public void Initialize()
    {
        _currentStamina = _maxStamina;
        _staminaSlider.Initialize();
        _staminaSlider.SetMaxValue(_maxStamina);

        SetupJumpVariables();
        _controller = GetComponent<CharacterController>();
        _playerTransform = GetComponent<Transform>();
        _playerVelocity = new Vector3(0, _gravity, 0);

        // Setup state
        _states = new PlayerStateFactory(this);
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

        _playerInput.PlayerControlls.Attack.started += OnAttackInput;
        _playerInput.PlayerControlls.Attack.canceled += OnAttackInput;

        // Animation
        _playerHeadIKAnim = GetComponent<HeadIKAnim>();
        _playerHeadIKAnim?.Initialize();
    }

    private void Update()
    {
        _currentState.UpdateStates();
        ApplyVelocityY();
        CoolDownDash();
        CoolDownAttack();
        FillStamina();
    }

    void FillStamina()
    {
        if (!_isDashing && Controller.isGrounded && _currentStamina < _maxStamina && !_isRunning)
        {
            _currentStamina += Time.deltaTime;
            _staminaSlider.SetValue(_currentStamina);
        }
        else if (_isRunning)
        {
            _currentStamina -= Time.deltaTime;
            _staminaSlider.SetValue(_currentStamina);
        }
    }

    public void ReduceStamina()
    {
        _currentStamina -= _staminaReducer;
        _staminaSlider.SetValue(_currentStamina);
    }

    void CoolDownDash()
    {
        _dashCooldownTimer += Time.deltaTime;
        if (_dashCooldownTimer >= _dashCooldown && !_isDashPressed && _currentStamina >= _staminaReducer)
            _canDash = true;
        else
            _canDash = false;
    }

    void CoolDownAttack()
    {
        _attackCooldownTimer += Time.deltaTime;
        if (_attackCooldownTimer >= _attackCooldown && !_isAttackPressed && _currentStamina >= _staminaReducer)
            _canAttack = true;
        else
            _canAttack = false;
    }

    private void SetupJumpVariables()
    {
        float timeToApex = _maxJumpTime / 2;
        _gravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        _initialJumpVelocity = (2 * _maxJumpHeight) / timeToApex;
    }

    public void ApplyRotation()
    {
        Vector3 targetPostition = new Vector3(_lookAtTarget.position.x,
                                       _playerTransform.position.y,
                                       _lookAtTarget.position.z);
        var targetRotation = Quaternion.LookRotation(targetPostition - _playerTransform.position);
        _playerTransform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotatingSpeed * Time.deltaTime);
    }

    public void ApplyModelRotation()
    {
        ApplyRotation();
        Quaternion toRotation;
        toRotation = Quaternion.LookRotation(_appliedMoveVelocity, Vector3.up);
        _model.rotation = Quaternion.RotateTowards(_model.rotation, toRotation, Time.deltaTime * _modelRotatingSpeed);
    }

    public void RevertModelRotation()
    {
        ApplyRotation();
        _model.rotation = Quaternion.RotateTowards(_model.rotation, PlayerTransform.localRotation, Time.deltaTime * _modelRotatingSpeed);
    }

    public void ApplyVelocityY() => _controller.Move(_playerVelocity * Time.deltaTime);

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _currentMovement.x = _currentMovementInput.x;
        _currentMovement.z = _currentMovementInput.y;
        _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
    }
    private void OnJumpInput(InputAction.CallbackContext context) => _isJumpingPressed = context.ReadValueAsButton();
    private void OnDashInput(InputAction.CallbackContext context)
    {
        _isDashPressed = context.ReadValueAsButton();
        _isRunningPressed = context.ReadValueAsButton();
    }

    private void OnAttackInput(InputAction.CallbackContext context) => _isAttackPressed = context.ReadValueAsButton();

    private void OnEnable() => _playerInput.PlayerControlls.Enable();
    private void OnDisable() => _playerInput.PlayerControlls.Disable();
}
