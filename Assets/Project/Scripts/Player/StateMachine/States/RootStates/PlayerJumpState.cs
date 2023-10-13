using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory stateFactory)
        : base(currentContext, stateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        HandleJump();
        Ctx.ReduceStamina();
        Ctx.IsJumping = true;
    }

    public override void UpdateState()
    {
        CheckSwitchState();
        HandleGravity();
        HandleMovement();
    }

    public override void ExitState() { }

    public override void CheckSwitchState()
    {
        if (Ctx.Controller.isGrounded)
        {
            SwitchState(Factory.Grounded());
            Ctx.IsJumping = false;
        }
    }

    public override void InitializeSubState()
    {

    }

    void HandleJump()
    {
        if (Ctx.Controller.isGrounded)
            Ctx.PlayerVelocityY = Ctx.InitialJumpVelocity;
    }

    void HandleMovement()
    {
        if (Ctx.IsDashing)
            Ctx.Controller.Move(Ctx.AppliedMoveVelocity * Time.deltaTime * Ctx.MovingSpeed * 3);
        else
            Ctx.Controller.Move(Ctx.AppliedMoveVelocity * Time.deltaTime * Ctx.MovingSpeed);
    }

    void HandleGravity()
    {
        Ctx.PlayerVelocityY += Ctx.Gravity * Time.deltaTime;
    }
}
