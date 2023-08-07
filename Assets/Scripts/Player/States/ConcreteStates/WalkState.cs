using UnityEngine;

public class WalkState : BaseState
{
    public WalkState(CharacterStateMachine currentContext, StateFactory stateFactory)
        : base(currentContext, stateFactory) { }

    public override void EnterState()
    {

    }

    public override void UpdateState()
    {
        CheckSwitchState();
        HandleMovement();
        Ctx.ApplyAIMRotation();
    }

    public override void ExitState() { }

    public override void CheckSwitchState()
    {
        if (!Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Idle());
        }
        else if (Ctx.IsDashPressed && Ctx.CanDash)
        {
            SwitchState(Factory.Dash());
        }
        else if (Ctx.IsAttackPressed && Ctx.CanAttack)
        {
            SwitchState(Factory.Attack());
        }
    }

    void HandleMovement()
    {
        Vector3 move = new Vector3(Ctx.CurrentMovementX, 0, Ctx.CurrentMovementZ);
        move = move.x * Ctx.CharacterTransform.right.normalized + move.z * Ctx.CharacterTransform.forward.normalized;
        move.y = 0f;
        Ctx.Controller.Move(move * Time.deltaTime * Ctx.MoveingSpeed);
        Ctx.AppliedMoveVelocity = move;
    }

    public override void InitializeSubState() { }
}