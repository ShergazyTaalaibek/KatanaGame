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
        timer = 0;
        Ctx.DashCooldownTimer = 0;
    }

    public override void UpdateState()
    {
        if (timer >= Ctx.DashingTime)
        {
            CheckSwitchState();
        }
        HandleMovement();
        timer += Time.deltaTime;
    }

    public override void ExitState() { }

    public override void CheckSwitchState()
    {
        if (!Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Idle());
        }
        else
        {
            SwitchState(Factory.Walk());
        }
    }

    public override void InitializeSubState() { }

    void HandleMovement()
    {
        Ctx.Controller.Move(Ctx.AppliedMoveVelocity * Time.deltaTime * Ctx.DashSpeed);
    }
}
