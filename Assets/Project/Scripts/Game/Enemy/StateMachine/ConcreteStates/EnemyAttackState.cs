using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyStateMachine currentContext, EnemyStateFactory stateFactory)
       : base(currentContext, stateFactory) { }

    public override void EnterState()
    {
        Ctx.AttackDurationTimer = 0;
        Ctx.SetAttackSpeed();
        Ctx.StartAttackCoroutine();
    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void ExitState()
    {

    }

    public override void CheckSwitchState()
    {
        if (Ctx.AttackDurationTimer >= Ctx.AttackDuration)
        {
            SwitchState(Factory.Idle());
            Ctx.EnemyCombatSystem.IterateChain();
        }
    }

    public override void InitializeSubState() { }
}