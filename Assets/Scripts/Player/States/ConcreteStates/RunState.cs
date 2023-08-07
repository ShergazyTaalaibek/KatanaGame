using UnityEngine;

public class RunState : BaseState
{
    public RunState(CharacterStateMachine currentContext, StateFactory stateFactory)
        : base(currentContext, stateFactory) { }

    public override void EnterState()
    {
        Debug.Log("Run state");
        Ctx.IsRunning = true;
    }

    public override void UpdateState()
    {
        CheckSwitchState();
        HandleMovement();
        //Ctx.ApplyModelRotation();
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
            ExitState();
        }
        else if (!Ctx.IsRunningPressed)
        {
            SwitchState(Factory.Walk());
            ExitState();
        }
        else if (Ctx.IsJumping && Ctx.CurrentStamina >= Ctx.StaminaReducer)
        {
            SwitchState(Factory.Jump());
            ExitState();
        }
        else if (Ctx.IsAttackPressed && Ctx.CanAttack)
        {
            SwitchState(Factory.Attack());
            ExitState();
        }
    }

    void HandleMovement()
    {
        Vector3 move = new Vector3(Ctx.CurrentMovementX, 0, Ctx.CurrentMovementZ);
        move = move.x * Ctx.CharacterTransform.right.normalized + move.z * Ctx.CharacterTransform.forward.normalized;
        move.y = 0f;
        Ctx.Controller.Move(move * Time.deltaTime * Ctx.RunningSpeed);
        Ctx.AppliedMoveVelocity = move;
    }

    public override void InitializeSubState() { }
}
