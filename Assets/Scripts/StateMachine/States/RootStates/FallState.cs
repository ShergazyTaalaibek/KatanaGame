using UnityEngine;

public class FallState : BaseState
{
    public FallState(PlayerStateMachine currentContext, StateFactory stateFactory)
       : base(currentContext, stateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        Debug.Log("Dead");
    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {

    }

    public override void CheckSwitchState()
    {

    }

    public override void InitializeSubState()
    {

    }
}
