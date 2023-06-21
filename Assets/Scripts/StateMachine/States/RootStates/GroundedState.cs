using UnityEngine;

public class GroundedState : BaseState
{
    public GroundedState(PlayerStateMachine currentContext, StateFactory stateFactory)
        : base (currentContext, stateFactory)
    {
        IsRootState = true;
        InitializeSubState(); 
    }

    public override void EnterState()
    {
        Ctx.PlayerVelocityY = Ctx.GroundedGravity;
    }

    public override void UpdateState()
    {
        HandleGravity();
        CheckSwitchState();
    }

    public override void ExitState() { }

    public override void CheckSwitchState()
    {
        if (Ctx.IsJumping)
        {
            SwitchState(Factory.JumpState());
        }
    }

    public override void InitializeSubState()
    {
        if (Ctx.IsMovementPressed && !Ctx.IsDashing)
        {
            SetSubState(Factory.Walk());
        }
        else if (Ctx.IsMovementPressed && Ctx.IsDashing)
        {
            SetSubState(Factory.Dash());
        }
        else
        {
            SetSubState(Factory.Idle());
        }
    }

    void HandleGravity()
    {
        if (Ctx.Controller.isGrounded)
            Ctx.PlayerVelocityY = Ctx.GroundedGravity;
        else
            Ctx.PlayerVelocityY += Ctx.Gravity * Time.deltaTime;
    }
}
