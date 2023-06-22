using System.Collections;
using UnityEngine;

public class DashState : BaseState
{
    public DashState(PersonStateMachine currentContext, StateFactory stateFactory)
        : base(currentContext, stateFactory) { }

    private float timer = 0;

    public override void EnterState()
    {
        timer = 0;
        PlayerCtx.CurrentStamina -= PlayerCtx.StaminaReducer;
        PlayerCtx.DashCooldownTimer = 0;
        Ctx.IsDashing = true;
    }

    public override void UpdateState()
    {
        if (timer >= Ctx.DashingTime)
        {
            CheckSwitchState();
        }
        else if (Ctx.IsJumping)
        {
            CheckSwitchState();
        }
        HandleMovement();
        timer += Time.deltaTime;
    }

    public override void ExitState() { }

    public override void CheckSwitchState()
    {
        if (Ctx.IsJumping)
        {
            SwitchState(Factory.JumpState());
        }
        else if (Ctx.IsMovementPressed)
        {
            Ctx.IsDashing = false;
            SwitchState(Factory.Walk());
        }
        else
        {
            Ctx.IsDashing = false;
            SwitchState(Factory.Idle());
        }
    }

    public override void InitializeSubState() { }

    void HandleMovement()
    {
        Ctx.Controller.Move(Ctx.AppliedMoveVelocity * Time.deltaTime * Ctx.DashSpeed);
    }
}
