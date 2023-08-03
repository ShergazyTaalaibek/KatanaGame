using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform _cameraTransform;

    [Header("States")]
    private BaseState _currentState;

    private PlayerInput playerInput;
    private Vector2 _currentMovementInput;
    private Vector3 _currentMovement;
    private bool _isMovementPressed;
    private bool _isJumping;
    private bool _isDashing;

    private Animator animator;

    public void Initialize()
    {
        playerInput = new PlayerInput();
        playerInput.PlayerControlls.Move.started += OnMovementInput;
        playerInput.PlayerControlls.Move.canceled += OnMovementInput;
        playerInput.PlayerControlls.Move.performed += OnMovementInput;
        playerInput.PlayerControlls.Jump.started += OnJumpInput;
        playerInput.PlayerControlls.Jump.canceled += OnJumpInput;
        playerInput.PlayerControlls.Dash.started += OnDashInput;
        playerInput.PlayerControlls.Dash.canceled += OnDashInput;

        controller = GetComponent<CharacterController>();
        _cameraTransform = Camera.main.transform;

        animator = GetComponent<Animator>();
    }

    //private void HandleAnimation()
    //{
    //    bool _isWalking = animator.GetBool("isWalking");
    //    bool _isDashing = animator.GetBool("isDashing");

    //    if (_isMovementPressed && !_isWalking)
    //    {
    //        animator.SetBool("isWalking", true);
    //    }

    //    else if (!_isMovementPressed && _isWalking)
    //    {
    //        animator.SetBool("isWalking", false);
    //    }
    //}

    private void Update()
    {
        ApplyMovement();

        if (_isJumping && groundedPlayer)
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);

        controller.Move(playerVelocity * Time.deltaTime);
        ApplyGravity();
    }

    private void ApplyMovement()
    {
        Vector3 move = new Vector3(_currentMovement.x, 0, _currentMovement.z);
        move = move.x * _cameraTransform.right.normalized + move.z * _cameraTransform.forward.normalized;
        move.y = 0f;
        controller.Move(move.normalized * Time.deltaTime * playerSpeed);
    }

    private void ApplyGravity()
    { 
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
            playerVelocity.y = -.05f;
        else
            playerVelocity.y += gravityValue * Time.deltaTime;
    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _currentMovement.x = _currentMovementInput.x;
        _currentMovement.z = _currentMovementInput.y;
        _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
    }
    private void OnJumpInput(InputAction.CallbackContext context) =>_isJumping = context.ReadValueAsButton();
    private void OnDashInput(InputAction.CallbackContext context) => _isDashing = context.ReadValueAsButton();

    private void OnEnable() => playerInput.PlayerControlls.Enable();
    private void OnDisable() => playerInput.PlayerControlls.Disable();
}
