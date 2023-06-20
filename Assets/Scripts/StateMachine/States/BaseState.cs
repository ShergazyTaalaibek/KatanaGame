public abstract class BaseState
{
    protected PlayerStateMachine _ctx;
    protected StateFactory _factory;
    public BaseState(PlayerStateMachine currentContext, StateFactory stateFactory)
    {
        _ctx = currentContext;
        _factory = stateFactory;
    }

    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();

    public abstract void CheckSwitchState();

    public abstract void InitializeSubState();

    void UpdateStates() { }

    protected void SwitchState(BaseState newState)
    {
        // Current state exits state
        ExitState();

        // New state enters state
        newState.EnterState();

        // Switch current state of context
        _ctx.CurrentState = newState;
    }

    protected void SetSuperState() { }

    protected void SetSubState() { }
}


// --- For concrete states:

//public override void EnterState() {}

//public override void UpdateState() {}

//public override void ExitState() {}

//public override void CheckSwitchState() {}

//public override void InitializeSubState() {}