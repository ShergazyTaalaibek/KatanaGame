using UnityEngine;

public class WalkState : BaseState
{
    public WalkState(PlayerStateMachine currentContext, StateFactory stateFactory)
        : base(currentContext, stateFactory) { }

    public override void EnterState()
    {
        Debug.Log("Walk");
    }

    public override void UpdateState()
    {
        CheckSwitchState();
        HandleMovement();
        Debug.Log("Walk");
    }

    public override void ExitState() { }

    public override void CheckSwitchState()
    {
        if (!Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Idle());
        }
        if (Ctx.IsDashing)
        {
            SwitchState(Factory.Dash());
        }
    }

    void HandleMovement()
    {
        Vector3 move = new Vector3(Ctx.CurrentMovementX, 0, Ctx.CurrentMovementZ);
        move = move.x * Ctx.CameraTransform.right.normalized + move.z * Ctx.CameraTransform.forward.normalized;
        move.y = 0f;
        Ctx.Controller.Move(move * Time.deltaTime * Ctx.MoveingSpeed);
    }

    public override void InitializeSubState() { }
}
