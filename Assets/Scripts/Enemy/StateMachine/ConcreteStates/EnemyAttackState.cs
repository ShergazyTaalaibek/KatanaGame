using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyStateMachine currentContext, EnemyStateFactory stateFactory)
       : base(currentContext, stateFactory) { }

    public override void EnterState()
    {
        Ctx.AttackDurationTimer = 0;
        Debug.Log("Enemy Attack");
    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void ExitState() { }

    public override void CheckSwitchState()
    {
        if (Ctx.AttackDurationTimer >= Ctx.AttackDuration)
        {
            Ctx.IsAttacking = false;
            SwitchState(Factory.Idle());
        }
    }

    public override void InitializeSubState() { }
}
