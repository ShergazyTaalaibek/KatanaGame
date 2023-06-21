using UnityEngine;

public class JumpState : BaseState
{
    public JumpState(PlayerStateMachine currentContext, StateFactory stateFactory)
        : base(currentContext, stateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        HandleJump();
        Debug.Log("Jump");
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
        }
    }

    public override void InitializeSubState()
    {

    }

    void HandleJump()
    {
        if (Ctx.Controller.isGrounded)
        {
            Ctx.PlayerVelocityY = Ctx.InitialJumpVelocity;
        }
    }

    void HandleMovement()
    {
        Ctx.Controller.Move(Ctx.AppliedMoveVelocity * Time.deltaTime * Ctx.MoveingSpeed);
    }

    void HandleGravity()
    {
        Ctx.PlayerVelocityY += Ctx.Gravity * Time.deltaTime;    
    }
}
