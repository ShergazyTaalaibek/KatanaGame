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
    private PlayerInput playerInput;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    [Header("States")]
    private BaseState baseState;
    public DeadState deadState = new DeadState();

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _dashAction;
    private InputAction _slashAction;

    public void Initialize()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        _moveAction = playerInput.actions["Move"];
        _jumpAction = playerInput.actions["Jump"];
        _dashAction = playerInput.actions["Dash"];
        _slashAction = playerInput.actions["Slash"];
    }

    private void Update()
    {
        groundedPlayer = controller.isGrounded;

        Vector2 input = _moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        ApplyGravity();
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void ApplyGravity()
    { 
        if (groundedPlayer && playerVelocity.y < 0)
            playerVelocity.y = 0f;
        else
            playerVelocity.y += gravityValue * Time.deltaTime;
    }


}
