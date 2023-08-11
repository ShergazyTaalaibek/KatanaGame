using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory stateFactory)
        : base(currentContext, stateFactory) { }

    public override void EnterState()
    {
        Ctx.IsRunning = true;
    }

    public override void UpdateState()
    {
        CheckSwitchState();
        HandleMovement();
        Ctx.ApplyRotation();
        Ctx.ApplyModelRotation();
    }

    public override void ExitState()
    {
        Ctx.IsRunning = false;
    }

    public override void CheckSwitchState()
    {
        if (!Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Idle());
        }
        else if (!Ctx.IsRunningPressed || Ctx.CurrentStamina <= 0)
        {
            SwitchState(Factory.Walk());
        }
        else if (Ctx.IsJumpingPressed && Ctx.CurrentStamina >= Ctx.StaminaReducer)
        {
            SwitchState(Factory.Jump());
        }
        else if (Ctx.IsAttackPressed && Ctx.CanAttack)
        {
            SwitchState(Factory.Attack());
        }
    }

    void HandleMovement()
    {
        Vector3 move = new Vector3(Ctx.CurrentMovementX, 0, Ctx.CurrentMovementZ);
        move = move.x * Ctx.PlayerTransform.right.normalized + move.z * Ctx.PlayerTransform.forward.normalized;
        move.y = 0f;
        Ctx.Controller.Move(move * Time.deltaTime * Ctx.RunningSpeed);
        Ctx.AppliedMoveVelocity = move;
    }

    public override void InitializeSubState() { }
}
