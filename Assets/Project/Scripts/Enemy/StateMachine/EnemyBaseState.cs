public abstract class EnemyBaseState
{
    private bool _isRootState = false;
    private EnemyStateMachine _ctx;
    private EnemyStateFactory _factory;
    private EnemyBaseState _currentSuperState;
    private EnemyBaseState _currentSubState;

    protected bool IsRootState { set { _isRootState = value; } }
    protected EnemyStateMachine Ctx { get { return _ctx; } }
    protected EnemyStateFactory Factory { get { return _factory; } }

    public EnemyBaseState(EnemyStateMachine currentContext, EnemyStateFactory stateFactory)
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
            _currentSubState.UpdateStates();
    }

    protected void SwitchState(EnemyBaseState newState)
    {
        // === vvv === Current state exits state
        ExitState();

        // === vvv === New state enters state
        newState.EnterState();

        // === vvv === Switch current state of context
        if (_isRootState)
            Ctx.CurrentState = newState;
        else if (_currentSuperState != null)
            _currentSuperState.SetSubState(newState);
    }

    protected void SetSuperState(EnemyBaseState newSuperState) => _currentSuperState = newSuperState;

    protected void SetSubState(EnemyBaseState newSubState)
    {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
}
