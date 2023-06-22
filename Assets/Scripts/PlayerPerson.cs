using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPerson : PersonStateMachine
{
    // Stamina
    [SerializeField, Range(1, 10)] private float _maxStamina = 5f;
    [SerializeField, Range(0, 10)] private float _staminaReducer = 5f;
    [SerializeField] private float _currentStamina;
    // Dash
    [SerializeField, Range(0f, 10f)] private float _dashCooldown = 1f;
    private float _dashCooldownTimer = 0;
    // Input system
    private PlayerInput _playerInput;


    // Getters & setters
    // Stamina
    public float CurrentStamina { get { return _currentStamina; } set { _currentStamina = value; } }
    public float StaminaReducer { get { return _staminaReducer; } }
    // Dash
    public float DashCooldownTimer { set { _dashCooldownTimer = value; } }

    public override void Initialize()
    {
        base.Initialize();
        SetupStateMachine();

        _isPlayer = true;
        _currentStamina = _maxStamina;

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

    void FillStamina()
    {
        if (!_isDashing && Controller.isGrounded && _currentStamina < _maxStamina)
        {
            _currentStamina += Time.deltaTime;
        }
    }

    void CoolDownDash()
    {
        _dashCooldownTimer += Time.deltaTime;
        if (_dashCooldownTimer >= _dashCooldown && !_isDashPressed && _currentStamina >= _staminaReducer)
            _canDash = true;
        else
            _canDash = false;
    }

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
