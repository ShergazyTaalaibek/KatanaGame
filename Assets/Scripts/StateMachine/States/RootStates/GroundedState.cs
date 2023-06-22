using UnityEngine;

public class GroundedState : BaseState
{
    public GroundedState(PersonStateMachine currentContext, StateFactory stateFactory)
        : base (currentContext, stateFactory)
    {
        IsRootState = true;
        InitializeSubState(); 
    }

    public override void EnterState()
    {
        Ctx.PlayerVelocityY = Ctx.GroundedGravity;
        Ctx.AppliedMoveVelocity = Vector3.zero;
    }

    public override void UpdateState()
    {
        HandleGravity();
        CheckSwitchState();
        Ctx.ApplyPersonRotation();
    }

    public override void ExitState() { }

    public override void CheckSwitchState()
    {
        if (Ctx.IsPlayer)
        {
            if (PlayerCtx.CurrentStamina >= PlayerCtx.StaminaReducer)
            {
                SwitchState(Factory.JumpState());
            }
        }
        else
        {
            if (Ctx.IsJumping)
            {
                SwitchState(Factory.JumpState());
            }
        }
    }

    public override void InitializeSubState()
    {
        if (Ctx.IsMovementPressed)
        {
            SetSubState(Factory.Walk());
        }
        else if (Ctx.IsMovementPressed && Ctx.IsDashPressed && Ctx.CanDash)
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
