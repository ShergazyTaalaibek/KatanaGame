using System.Collections;
using UnityEngine;

public class DashState : BaseState
{
    public DashState(PlayerStateMachine currentContext, StateFactory stateFactory)
        : base(currentContext, stateFactory) { }

    private float timer = 0;

    public override void EnterState()
    {
        Debug.Log("Dash");
        Ctx.ReduceStamina();
        timer = 0;
        Ctx.DashCooldownTimer = 0;
        Ctx.IsDashing = true;
        Ctx.CanDash = false;
    }

    public override void UpdateState()
    {
        if (timer >= Ctx.DashingTime || Ctx.IsJumping)
        {
            CheckSwitchState();
        }
        else
        {
            HandleMovement();
            timer += Time.deltaTime;
        }
    }

    public override void ExitState() { }

    public override void CheckSwitchState()
    {
        if (!Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Idle());
            Ctx.IsDashing = false;
        }
        else
        {
            SwitchState(Factory.Walk());
            Ctx.IsDashing = false;
        }
    }

    public override void InitializeSubState() { }

    void HandleMovement()
    {
        Ctx.Controller.Move(Ctx.AppliedMoveVelocity * Time.deltaTime * Ctx.DashSpeed);
    }
}
