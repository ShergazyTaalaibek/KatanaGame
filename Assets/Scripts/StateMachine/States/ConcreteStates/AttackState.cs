using UnityEngine;

public class AttackState : BaseState
{
    public AttackState(CharacterStateMachine currentContext, StateFactory stateFactory)
       : base(currentContext, stateFactory) { }

    private float timer = 0;

    public override void EnterState()
    {
        Debug.Log("Slash");
        Ctx.ReduceStamina();
        timer = 0;
        Ctx.AttackCooldownTimer = 0;
        Ctx.IsAttacking = true;
        Ctx.CanAttack = false;
    }

    public override void UpdateState()
    {
        if (timer >= Ctx.AttackDuration || Ctx.IsJumping)
        {
            CheckSwitchState();
        }
        else
        {
            HandleAttack();
            timer += Time.deltaTime;
        }
    }

    public override void ExitState() { }

    public override void CheckSwitchState()
    {
        if (!Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Idle());
        }
        else
        {
            SwitchState(Factory.Walk());
        }
        Ctx.IsAttacking = false;
    }

    public override void InitializeSubState() { }

    private void HandleAttack()
    {
        Debug.Log("Slashed");
    }
}
