public abstract class BaseState
{
    private bool _isRootState = false;
    private PersonStateMachine _ctx;
    private PlayerPerson _playerCtx;
    private EnemyPerson _enemyCtx;
    private StateFactory _factory;
    private BaseState _currentSuperState;
    private BaseState _currentSubState;

    protected bool IsRootState { set { _isRootState = value; } }
    protected PersonStateMachine Ctx { get { return _ctx; } }
    protected PlayerPerson PlayerCtx { get { return _playerCtx; } }
    protected EnemyPerson EnemyCtx { get { return _enemyCtx; } }
    protected StateFactory Factory { get { return _factory; } }

    public BaseState(PersonStateMachine currentContext, StateFactory stateFactory)
    {
        _ctx = currentContext;
        _factory = stateFactory;
    }

    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();

    public abstract void CheckSwitchState();

    public abstract void InitializeSubState();

    public void UpdateStates()
    {
        UpdateState();
        if (_currentSubState != null)
        {
            _currentSubState.UpdateStates();
        }
    }

    protected void SwitchState(BaseState newState)
    {
        // Current state exits state
        ExitState();

        // New state enters state
        newState.EnterState();

        // Switch current state of context
        if (_isRootState)
        {
            Ctx.CurrentState = newState;
        }
        else if (_currentSuperState != null)
        {
            _currentSuperState.SetSubState(newState);
        }
    }

    protected void SetSuperState(BaseState newSuperState)
    {
        _currentSuperState = newSuperState;
    }

    protected void SetSubState(BaseState newSubState)
    {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
}


// --- For concrete states:

//public override void EnterState() {}

//public override void UpdateState() {}

//public override void ExitState() {}

//public override void CheckSwitchState() {}

//public override void InitializeSubState() {}