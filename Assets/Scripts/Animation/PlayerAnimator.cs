using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private CharacterStateMachine _character;
    private int XAxis, YAxis,JumpAnim, FallAnim, LandAnim, DashingAnim, AttackAnim;
    private float XVelocity = 0;
    private float YVelocity = 0;

    public void Initialize()
    {
        _character = GetComponent<CharacterStateMachine>();
        XAxis = Animator.StringToHash("XAxis");
        YAxis = Animator.StringToHash("YAxis");
        DashingAnim = Animator.StringToHash("IsDashing");
        JumpAnim = Animator.StringToHash("IsJumping");
        FallAnim = Animator.StringToHash("IsFalling");
        LandAnim = Animator.StringToHash("IsLanded");
        AttackAnim = Animator.StringToHash("IsAttack");
    }

    private void Update()
    {
        DoWalkAnim();
        DoDashAnim();
        DoJumpAnim();
        DoFallAnim();
        DoLandAnim();
        DoAttackAnim();
    }

    private void DoWalkAnim()
    {
        LerpVelocityies();
        anim.SetFloat(XAxis, XVelocity);
        anim.SetFloat(YAxis, YVelocity);
    }

    private void LerpVelocityies()
    {
        XVelocity = Mathf.Lerp(XVelocity, _character.CurrentMovementInputX, .05f);
        YVelocity = Mathf.Lerp(YVelocity, _character.CurrentMovementInputY, .05f);
    }

    private void DoJumpAnim() => anim.SetBool(JumpAnim, _character.IsJumping);
    private void DoDashAnim() => anim.SetBool(DashingAnim, _character.IsDashing);
    private void DoAttackAnim() => anim.SetBool(AttackAnim, _character.IsAttacking);
    private void DoFallAnim() => anim.SetBool(FallAnim, !_character.Controller.isGrounded);
    private void DoLandAnim() => anim.SetBool(LandAnim, _character.Controller.isGrounded);
}
