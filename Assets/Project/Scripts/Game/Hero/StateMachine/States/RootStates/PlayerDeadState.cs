using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    public PlayerDeadState(PlayerStateMachine currentContext, PlayerStateFactory stateFactory)
        : base(currentContext, stateFactory) { }

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
