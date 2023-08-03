using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private CharacterStateMachine _character;
    private int XAxis, YAxis, JumpAnim;
    private float XVelocity = 0;
    private float YVelocity = 0;

    public void Initialize()
    {
        _character = GetComponent<CharacterStateMachine>();
        XAxis = Animator.StringToHash("XAxis");
        YAxis = Animator.StringToHash("YAxis");
        JumpAnim = Animator.StringToHash("IsJumping");
    }

    public void DoWalkAnimation()
    {
        LerpVelocityies();
        anim.SetFloat(XAxis, XVelocity);
        anim.SetFloat(YAxis, YVelocity);
    }

    public void DoJumpAnim()
    {
        anim.SetBool(JumpAnim, _character.IsJumping);
    }

    private void LerpVelocityies()
    {
        XVelocity = Mathf.Lerp(XVelocity, _character.CurrentMovementInputX, .05f);
        YVelocity = Mathf.Lerp(YVelocity, _character.CurrentMovementInputY, .05f);
    }
}
