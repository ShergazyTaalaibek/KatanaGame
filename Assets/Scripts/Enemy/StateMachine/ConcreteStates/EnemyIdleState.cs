using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine currentContext, EnemyStateFactory stateFactory)
       : base(currentContext, stateFactory) { }

    public override void EnterState()
    {
        Debug.Log("Enemy Idle");
    }

    public override void UpdateState()
    {
        CheckSwitchState();
        Ctx.ApplyRotation();
    }

    public override void ExitState() { }

    public override void CheckSwitchState()
    {
        if (Ctx.IsMoving)
        {
            SwitchState(Factory.Walk());
        }
        else if (Ctx.IsAttacking)
        {
            SwitchState(Factory.Attack());
        }
    }

    public override void InitializeSubState() { }
}
