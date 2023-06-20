using UnityEngine;

public class StateFactory
{
    PlayerStateMachine _context;

    public StateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;
    }

    public BaseState Idle()
    {
        return new IdleState(_context, this);
    }

    public BaseState Walk()
    {
        return new WalkState(_context, this);
    }

    public BaseState JumpState()
    {
        return new JumpState(_context, this);
    }

    public BaseState Dash()
    {
        return new DashState(_context, this);
    }

    public BaseState Death()
    {
        return new DeadState(_context, this);
    }

    public BaseState Grounded()
    {
        return new GroundedState(_context, this);
    }
}
