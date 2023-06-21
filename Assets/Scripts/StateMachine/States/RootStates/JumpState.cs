using UnityEngine;

public class JumpState : BaseState
{
    public JumpState(PlayerStateMachine currentContext, StateFactory stateFactory)
        : base(currentContext, stateFactory)
    {
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
            Ctx.PlayerVelocityY = Ctx.InitialJumpVelocity;
    }

    void HandleGravity()
    {
        Ctx.PlayerVelocityY += Ctx.Gravity * Time.deltaTime;    
    }
}
