using UnityEngine;

public class DeadState : BaseState
{
    public override void EnterState()
    {
        Debug.Log("I'm dead");
    }

    public override void UpdateState() { }
}
