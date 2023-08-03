using UnityEngine;

public class GroundedState : BaseState
{
    public GroundedState(CharacterStateMachine currentContext, StateFactory stateFactory)
        : base (currentContext, stateFactory)
    {
        IsRootState = true;
        InitializeSubState(); 
    }

    public override void EnterState()
    {
        Ctx.PlayerVelocityY = Ctx.GroundedGravity;
        Ctx.AppliedMoveVelocity = Vector3.zero;
        Debug.Log("Grounded");
    }

    public override void UpdateState()
    {
        HandleGravity();
        CheckSwitchState();
    }

    public override void ExitState() { }

    public override void CheckSwitchState()
    {
        if (Ctx.IsJumping && Ctx.CurrentStamina >= Ctx.StaminaReducer)
        {
            SwitchState(Factory.JumpState());
        }
    }

    public override void InitializeSubState()
    {
        if (Ctx.IsMovementPressed)
        {
            SetSubState(Factory.Walk());
            Debug.Log("InitWalkState");
        }
        else if (Ctx.IsMovementPressed && Ctx.IsDashPressed && Ctx.CanDash && Ctx.CurrentStamina >= Ctx.StaminaReducer)
        {
            SetSubState(Factory.Dash());
            Debug.Log("InitDashState");
        }
        else if (Ctx.IsAttackPressed && Ctx.CanAttack)
        {
            SwitchState(Factory.Attack());
        }
        else
        {
            SetSubState(Factory.Idle());
            Debug.Log("InitIdleState");
        }
    }

    void HandleGravity()
    {
        if (Ctx.Controller.isGrounded)
            Ctx.PlayerVelocityY = Ctx.GroundedGravity;
        else
            Ctx.PlayerVelocityY += Ctx.Gravity * Time.deltaTime;
    }
}
