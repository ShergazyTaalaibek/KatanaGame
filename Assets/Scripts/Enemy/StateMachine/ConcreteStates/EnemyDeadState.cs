using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyStateMachine currentContext, EnemyStateFactory stateFactory)
       : base(currentContext, stateFactory) { }

    public override void EnterState()
    {

    }

    public override void UpdateState()
    {
        CheckSwitchState();
        //Ctx.ApplyAIMRotation();
    }

    public override void ExitState()
    {

    }

    public override void CheckSwitchState()
    {
        //if (Ctx.IsMovementPressed)
        //{
        //    SwitchState(Factory.Walk());
        //}
        //else if (Ctx.IsAttackPressed && Ctx.CanAttack)
        //{
        //    SwitchState(Factory.Attack());
        //}
    }

    public override void InitializeSubState()
    {

    }
}
