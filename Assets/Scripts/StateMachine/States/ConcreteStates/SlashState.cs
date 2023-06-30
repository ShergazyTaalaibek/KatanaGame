using UnityEngine;

public class SlashState : BaseState
{
    public SlashState(PlayerStateMachine currentContext, StateFactory stateFactory)
       : base(currentContext, stateFactory) { }

    public override void EnterState()
    {
        Debug.Log("Dead");
        Ctx.ReduceStamina();
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
