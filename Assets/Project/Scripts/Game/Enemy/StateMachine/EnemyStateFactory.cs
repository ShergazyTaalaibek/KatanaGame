using UnityEngine;

public class EnemyStateFactory
{
    EnemyStateMachine _context;

    public EnemyStateFactory(EnemyStateMachine currentContext) => _context = currentContext;

    public EnemyBaseState Grounded() => new EnemyGroundedState(_context, this);

    public EnemyBaseState Jump() => new EnemyJumpState(_context, this);

    public EnemyBaseState Cooldown() => new EnemyCooldownState(_context, this);

    public EnemyBaseState Dead() => new EnemyDeadState(_context, this);

    public EnemyBaseState Idle() => new EnemyIdleState(_context, this);

    public EnemyBaseState Walk() => new EnemyWalkState(_context, this);

    public EnemyBaseState Attack() => new EnemyAttackState(_context, this);
}
