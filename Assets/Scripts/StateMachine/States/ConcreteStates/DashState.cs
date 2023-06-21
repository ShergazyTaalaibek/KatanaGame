using UnityEngine;

public class DashState : BaseState
{
    public DashState(PlayerStateMachine currentContext, StateFactory stateFactory)
        : base(currentContext, stateFactory) { }

    public override void EnterState()
    {
        Debug.Log("Dash");
    }

    public override void UpdateState()
    {
        CheckSwitchState();
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
}
