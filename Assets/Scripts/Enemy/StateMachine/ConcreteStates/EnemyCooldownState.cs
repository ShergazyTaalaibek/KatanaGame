using UnityEngine;

public class EnemyCooldownState : EnemyBaseState
{
    public EnemyCooldownState(EnemyStateMachine currentContext, EnemyStateFactory stateFactory)
       : base(currentContext, stateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

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
        if (Ctx.CooldownTimer >= Ctx.CooldownDuration)
        {
            Ctx.IsCooldown = false;
            SwitchState(Factory.Grounded());
        }
    }

    public override void InitializeSubState()
    {
        if (Ctx.IsMoving)
        {
            SwitchState(Factory.Walk());
        }
    }
}
