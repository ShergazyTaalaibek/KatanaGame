using UnityEngine;

public class EnemyCooldownState : EnemyBaseState
{
    public EnemyCooldownState(EnemyStateMachine currentContext, EnemyStateFactory stateFactory)
       : base(currentContext, stateFactory) { }

    public override void EnterState()
    {
        Debug.Log("Enemy Cooldown");
    }

    public override void UpdateState()
    {
        CheckSwitchState();
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
