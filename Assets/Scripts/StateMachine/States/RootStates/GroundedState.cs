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
        Ctx.AppliedMoveVelocity = Vector3.zero;
        Debug.Log("Grounded");
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
        if (Ctx.IsMovementPressed)
        {
            SetSubState(Factory.Walk());
            Debug.Log("InitWalkState");
        }
        else if (Ctx.IsMovementPressed && Ctx.IsDashPressed && Ctx.CanDash)
        {
            SetSubState(Factory.Dash());
            Debug.Log("InitDashState");
        }
        else
        {
            SetSubState(Factory.Idle());
            Debug.Log("InitIdleState");
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
