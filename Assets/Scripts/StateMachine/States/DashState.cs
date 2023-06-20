using UnityEngine;

public class DashState : BaseState
{
    public DashState(PlayerStateMachine currentContext, StateFactory stateFactory)
        : base(currentContext, stateFactory) { }

    public override void EnterState() { }

    public override void UpdateState() { }

    public override void ExitState() { }

    public override void CheckSwitchState() { }

    public override void InitializeSubState() { }
}
