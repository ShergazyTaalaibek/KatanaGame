using UnityEngine;

public class PersonStateMachine : MonoBehaviour
{
    // --- Variables
    protected bool _isPlayer = false;
    // gravity
    private float _gravity = -9.8f;
    private float _groundedGravity = -.05f;
    // jumpin
    protected bool _isJumping = false;
    private float _initialJumpVelocity;
    [SerializeField, Range(1f, 5f)] private float _maxJumpHeight = 1.0f;
    [SerializeField, Range(.1f, 2f)] private float _maxJumpTime = .5f;
    // moving
    protected bool _isMovementPressed = false;
    private Vector3 _appliedMoveVelocity;
    [SerializeField] private float _movingSpeed = 2.0f;
    private CharacterController _controller;
    private Vector3 _playerVelocity;
    // Input system
    protected Vector2 _currentMovementInput;
    protected Vector3 _currentMovement;
    // dash
    protected bool _isDashPressed = false;
    protected bool _isDashing = false;
    protected bool _canDash = false;
    [SerializeField, Range(5f, 100f)] private float _dashSpeed = 10f;
    [SerializeField, Range(0f, 1f)] private float _dashingTime = 1f;
    // death
    private bool _isDead = false;
    // rotation
    private Transform _cameraTransform;
    private Transform _playerTransform;
    // State Machine
    private BaseState _currentState;
    private StateFactory _states;
    // Animation
    private Animator _animator;

    // --- Getters & setters
    public bool IsPlayer { get { return _isPlayer; } }
    public BaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
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
    public bool IsDashing { get { return _isDashing; } set { _isDashing = value; } }
    public bool CanDash { get { return _canDash; } }
    public float DashSpeed { get { return _dashSpeed; } }
    public float DashingTime { get { return _dashingTime; } }
    // Target
    public Transform CameraTransform { get { return _cameraTransform; } }
    public CharacterController Controller { get { return _controller; } }
    // Death
    public bool Isdead { get { return _isDead; } set { _isDead = value; } }
    // Animation
    public Animator Animator { get { return _animator; } }

    /// Init
    public virtual void Initialize()
    {
        SetupJumpVariables();

        _controller = GetComponent<CharacterController>();
        _cameraTransform = Camera.main.transform;
        _playerTransform = GetComponent<Transform>();
        _playerVelocity = new Vector3(0, _gravity, 0);
        _animator = GetComponent<Animator>();
    }

    public void SetupStateMachine()
    {
        _states = new StateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();
    }

    private void Update()
    {
        _currentState.UpdateStates();
        ApplyVelocityY();
        SetupJumpVariables(); // <-- must be disabled/commented
    }

    protected void SetupJumpVariables()
    {
        float timeToApex = _maxJumpTime / 2;
        _gravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        _initialJumpVelocity = (2 * _maxJumpHeight) / timeToApex;
    }

    public void ApplyVelocityY() => _controller.Move(_playerVelocity * Time.deltaTime);

    public void ApplyPersonRotation()
    {
        _playerTransform.rotation = Quaternion.Euler(0, _cameraTransform.rotation.eulerAngles.y, 0);
    }
}
