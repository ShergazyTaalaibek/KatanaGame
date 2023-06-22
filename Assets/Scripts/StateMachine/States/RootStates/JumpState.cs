using UnityEngine;

public class JumpState : BaseState
{
    public JumpState(PersonStateMachine currentContext, StateFactory stateFactory)
        : base(currentContext, stateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        HandleJump();
        PlayerCtx.CurrentStamina -= PlayerCtx.StaminaReducer;
    }

    public override void UpdateState()
    {
        CheckSwitchState();
        HandleGravity();
        HandleMovement();
        Ctx.ApplyPersonRotation();
    }

    public override void ExitState() { }

    public override void CheckSwitchState()
    {
        if (Ctx.Controller.isGrounded)
        {
            SwitchState(Factory.Grounded());
            Ctx.IsDashing = false;
        }
    }

    public override void InitializeSubState()
    {

    }

    void HandleJump()
    {
        if (Ctx.Controller.isGrounded)
        {
            Ctx.PlayerVelocityY = Ctx.InitialJumpVelocity;
        }
    }

    void HandleMovement()
    {
        if (Ctx.IsDashing)
        {
            Ctx.Controller.Move(Ctx.AppliedMoveVelocity * Time.deltaTime * Ctx.MoveingSpeed * 3);
        }
        else
        {
            Ctx.Controller.Move(Ctx.AppliedMoveVelocity * Time.deltaTime * Ctx.MoveingSpeed);
        }
    }

    void HandleGravity()
    {
        Ctx.PlayerVelocityY += Ctx.Gravity * Time.deltaTime;    
    }
}
