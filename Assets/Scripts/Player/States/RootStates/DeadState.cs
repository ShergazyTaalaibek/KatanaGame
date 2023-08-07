using UnityEngine;

public class DeadState : BaseState
{
    public DeadState(CharacterStateMachine currentContext, StateFactory stateFactory)
        : base(currentContext, stateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        Debug.Log("Dead");
        Ctx.HeadIKAnim.SetHeadIK(false);
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
