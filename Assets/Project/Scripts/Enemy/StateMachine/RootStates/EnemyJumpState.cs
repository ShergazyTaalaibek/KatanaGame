using UnityEngine;

public class EnemyJumpState : EnemyBaseState
{
    public EnemyJumpState(EnemyStateMachine currentContext, EnemyStateFactory stateFactory)
       : base(currentContext, stateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        HandleJump();
    }

    public override void UpdateState()
    {
        CheckSwitchState();
        HandleGravity();
        HandleMovement();
    }

    public override void ExitState() { }

    public override void CheckSwitchState()
    {
        if (Ctx.Controller.isGrounded)
        {
            SwitchState(Factory.Grounded());
            Ctx.IsJumping = false;
        }
    }

    public override void InitializeSubState() { }

    void HandleJump()
    {
        Ctx.EnemyVelocityY = Ctx.InitialJumpVelocity;
        Debug.Log(Ctx.EnemyVelocityY);
    }

    void HandleMovement()
    {
        Ctx.Controller.Move(Ctx.AppliedMoveVelocity * Time.deltaTime * Ctx.MovingSpeed);
    }

    void HandleGravity()
    {
        Ctx.EnemyVelocityY += Ctx.Gravity * Time.deltaTime;
    }
}
