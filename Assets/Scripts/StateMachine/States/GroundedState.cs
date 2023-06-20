using UnityEngine;

public class GroundedState : BaseState
{
    public GroundedState(PlayerStateMachine currentContext, StateFactory stateFactory)
        : base (currentContext, stateFactory) { }

    public override void EnterState()
    {
        Debug.Log("Grounded");
    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void ExitState() { }

    public override void CheckSwitchState()
    {
        if (_ctx.IsJumpPressed)
        {
            SwitchState(_factory.JumpState());
        }
    }

    public override void InitializeSubState() { }
}
