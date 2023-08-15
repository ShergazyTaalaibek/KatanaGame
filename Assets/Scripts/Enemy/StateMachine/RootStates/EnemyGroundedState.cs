using UnityEngine;

public class EnemyGroundedState : EnemyBaseState
{
    public EnemyGroundedState(EnemyStateMachine currentContext, EnemyStateFactory stateFactory)
       : base(currentContext, stateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        Ctx.EnemyVelocityY = Ctx.GroundedGravity;
        Ctx.AppliedMoveVelocity = Vector3.zero;
    }

    public override void UpdateState()
    {
        HandleGravity();
        CheckSwitchState();
    }

    public override void ExitState() { }

    public override void CheckSwitchState()
    {
        if (Ctx.IsJumping && Ctx.Controller.isGrounded)
        {
            SwitchState(Factory.Jump());
        }
        else if (Ctx.IsCooldown)
        {
            SwitchState(Factory.Cooldown());
        }
    }

    public override void InitializeSubState()
    {
        if (Ctx.IsMoving)
        {
            SetSubState(Factory.Walk());
        }
        else if (Ctx.IsAttacking)
        {
            SwitchState(Factory.Attack());
        }
        else
        {
            SetSubState(Factory.Idle());
        }
    }

    private void HandleGravity()
    {
        if (Ctx.Controller.isGrounded)
            Ctx.EnemyVelocityY = Ctx.GroundedGravity;
        else
            Ctx.EnemyVelocityY += Ctx.Gravity * Time.deltaTime;
    }
}
