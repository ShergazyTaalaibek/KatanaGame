using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    // === state machine
    private EnemyBaseState _currentState;
    private EnemyStateFactory _states;

    [Header("Others")]
    [SerializeField] private Transform _lookAtTarget;
    [SerializeField] private Transform _headTransform;
    [SerializeField] private float _rotatingSpeed;
    [SerializeField] private LayerMask _HurtLayerMask;
    private Transform _transform;
    private CharacterController _characterController;

    [Header("Movement")]
    [SerializeField] private bool _isMoving;
    [SerializeField] private float _movingSpeed = 2;
    [SerializeField, Range(-1, 1)] private float _movementInputX;
    [SerializeField, Range(-1, 1)] private float _movementInputY;
    private Vector3 _movementInput;
    private Vector3 _appliedMoveVelocity;

    [Header("Jumping")]
    [SerializeField] private bool _isJumping = false;
    [SerializeField] private float _initialJumpVelocity;
    private Vector3 _enemyVelocity;
    [SerializeField, Range(1f, 5f)] private float _maxJumpHeight = 1.0f;
    [SerializeField, Range(.1f, 2f)] private float _maxJumpTime = .5f;

    // gravity
    private float _gravity = -9.8f;
    private float _groundedGravity = -.05f;

    [Header("Attack")]
    [SerializeField] private bool _isAttacking = false;
    [SerializeField] private bool _canAttack = false;
    [SerializeField, Range(0f, 5f)] private float _attackDuration = 1f;
    [SerializeField] private float _attackSpeed;
    private float _attackDurationTimer = 0;
    [SerializeField] private WeaponCollision _swordCollision;
    private EnemyCombatSystem _enemyCombatSystem;

    // death
    [SerializeField] private bool _isDead = false;

    // === animation
    private HeadIKAnim _headIKAnim;

    // >>> Props
    // State Machine
    public EnemyBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public Transform EnemyTransform { get { return _transform; } }
    // Movement
    public bool IsMoving { get { return _isMoving; } }
    public float MovingSpeed { get { return _movingSpeed; } }
    public Vector3 MovementInput { get { return _movementInput; } }
    public Vector3 AppliedMoveVelocity { get { return _appliedMoveVelocity; } set { _appliedMoveVelocity = value; } }
    // Jump
    public bool IsJumping { get { return _isJumping; } set { _isJumping = value; } }
    public float InitialJumpVelocity { get { return _initialJumpVelocity; } }
    public float EnemyVelocityY { get { return _enemyVelocity.y; } set { _enemyVelocity.y = value; } }
    // Gravity
    public float Gravity { get { return _gravity; } }
    public float GroundedGravity { get { return _groundedGravity; } }
    // Attack
    public bool IsAttacking { get { return _isAttacking; } set { _isAttacking = value; } }
    public bool CanAttack { get { return _canAttack; } set { _canAttack = value; } }
    public float AttackDuration { get { return _attackDuration; } set { _attackDuration = value; } }
    public float AttackDurationTimer { get { return _attackDurationTimer; } set { _attackDurationTimer = value; } }
    public float AttackSpeed { get { return _attackSpeed; } }
    public EnemyCombatSystem EnemyCombatSystem { get { return _enemyCombatSystem; } }

    // Others
    public CharacterController Controller { get { return _characterController; } }

    public void Initialize()
    {
        SetupJumpVariables();

        _transform = GetComponent<Transform>();
        _characterController = GetComponent<CharacterController>();
        _enemyVelocity = new Vector3(0, _gravity, 0);

        // === Animation
        _headIKAnim = GetComponent<HeadIKAnim>();
        _headIKAnim?.Initialize();

        // === Setup State
        _states = new EnemyStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();

        _enemyCombatSystem = GetComponent<EnemyCombatSystem>();
    }

    private void Update()
    {
        _currentState.UpdateStates();
        ApplyVelocityY();
        SetMovementInput();
        AttackTimer();
    }

    private void AttackTimer()
    {
        _attackDurationTimer += Time.deltaTime;
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
                                       _transform.position.y,
                                       _lookAtTarget.position.z);
        var targetRotation = Quaternion.LookRotation(targetPostition - _transform.position);
        _transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotatingSpeed * Time.deltaTime);
    }

    private void SetMovementInput()
    {
        _movementInput.x = _movementInputX;
        _movementInput.z = _movementInputY;
    }

    public void ApplyVelocityY() => _characterController.Move(_enemyVelocity * Time.deltaTime);

    public void SetAttackSpeed()
    {
        _attackSpeed = _enemyCombatSystem.GetAnimationLength() / _attackDuration;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((_HurtLayerMask.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            Debug.Log("Sword hit");
        }
    }
}
