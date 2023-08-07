using UnityEngine;

public class AttackState : BaseState
{
    public AttackState(CharacterStateMachine currentContext, StateFactory stateFactory)
       : base(currentContext, stateFactory) { }

    private float timer = 0;

    public override void EnterState()
    {
        Ctx.ReduceStamina();
        timer = 0;
        Ctx.AttackCooldownTimer = 0;
        Ctx.IsAttacking = true;
        Ctx.CanAttack = false;
        Ctx.SwordCollision.SetActiveCollision(true);
    }

    public override void UpdateState()
    {
        if (timer >= Ctx.AttackDuration || Ctx.IsJumping)
        {
            CheckSwitchState();
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    public override void ExitState() { }

    public override void CheckSwitchState()
    {
        Ctx.SwordCollision.SetActiveCollision(false);
        Ctx.IsAttacking = false;
        if (!Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Idle());
        }
        else
        {
            SwitchState(Factory.Walk());
        }
    }

    public override void InitializeSubState() { }
}
