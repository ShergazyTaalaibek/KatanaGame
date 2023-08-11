using UnityEngine;

public class EnemyWalkState : EnemyBaseState
{
    public EnemyWalkState(EnemyStateMachine currentContext, EnemyStateFactory stateFactory)
       : base(currentContext, stateFactory) { }

    public override void EnterState()
    {
        Debug.Log("Enemy Walk");
    }

    public override void UpdateState()
    {
        CheckSwitchState();
        HandleMovement();
        Ctx.ApplyRotation();
    }

    public override void ExitState() { }

    public override void CheckSwitchState()
    {
        if (!Ctx.IsMoving)
        {
            SwitchState(Factory.Idle());
        }
        else if (Ctx.IsAttacking)
        {
            SwitchState(Factory.Attack());
        }
    }

    private void HandleMovement()
    {
        Vector3 move = Ctx.MovementInput;
        move = move.x * Ctx.EnemyTransform.right.normalized + move.z * Ctx.EnemyTransform.forward.normalized;
        move.y = 0f;
        Ctx.Controller.Move(move * Time.deltaTime * Ctx.MovingSpeed);
        Ctx.AppliedMoveVelocity = move;
    }

    public override void InitializeSubState() { }
}
