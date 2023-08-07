using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(CharacterStateMachine currentContext, StateFactory stateFactory)
        : base(currentContext, stateFactory) { }

    public override void EnterState()
    {

    }

    public override void UpdateState()
    {
        CheckSwitchState();
        Ctx.ApplyAIMRotation();
    }

    public override void ExitState()
    {

    }

    public override void CheckSwitchState()
    {
        if (Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Walk());
        }
        else if (Ctx.IsAttackPressed && Ctx.CanAttack)
        {
            SwitchState(Factory.Attack());
        }
    }

    public override void InitializeSubState()
    {

    }
}