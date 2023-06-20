using UnityEngine;

public class JumpState : BaseState
{
    public JumpState(PlayerStateMachine currentContext, StateFactory stateFactory)
        : base(currentContext, stateFactory) { }

    public override void EnterState()
    {
        HandleJump();
    }

    public override void UpdateState()
    {
        _ctx.ApplyVelocityY();
    }

    public override void ExitState() { }

    public override void CheckSwitchState() { }

    public override void InitializeSubState() {}

    void HandleJump()
    {
        _ctx.PlayerVelocityY += Mathf.Sqrt(_ctx.PlayerJumpHeight * -3.0f * _ctx.GravityValue);
    }
}
