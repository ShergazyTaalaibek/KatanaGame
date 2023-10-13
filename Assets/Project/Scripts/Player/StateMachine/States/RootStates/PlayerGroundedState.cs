using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory stateFactory)
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
    }

    public override void ExitState() { }

    public override void CheckSwitchState()
    {
        if (Ctx.IsJumpingPressed && Ctx.CurrentStamina >= Ctx.StaminaReducer)
        {
            SwitchState(Factory.Jump());
        }
    }

    public override void InitializeSubState()
    {
        if (Ctx.IsMovementPressed)
        {
            SetSubState(Factory.Walk());
        }
        else if (Ctx.IsMovementPressed && Ctx.IsDashPressed && Ctx.CanDash && Ctx.CurrentStamina >= Ctx.StaminaReducer)
        {
            SetSubState(Factory.Dash());
        }
        else if (Ctx.IsAttackPressed && Ctx.CanAttack)
        {
            SwitchState(Factory.Attack());
        }
        else
        {
            SetSubState(Factory.Idle());
        }
    }

    private void HandleGravity()
    {
        if (Ctx.Controller.isGrounded)
            Ctx.PlayerVelocityY = Ctx.GroundedGravity;
        else
            Ctx.PlayerVelocityY += Ctx.Gravity * Time.deltaTime;
    }
}
