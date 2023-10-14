using UnityEngine;

public class HeroAnimatorController : MonoBehaviour
{
    private Animator _anim;
    private PlayerStateMachine _character;
    private int _XAxis, _YAxis, _JumpAnim, _FallAnim, _LandAnim, _DashingAnim, _AttackAnim, _RunAnim;
    private float _XVelocity = 0;
    private float _YVelocity = 0;

    public void Initialize()
    {
        _anim = GetComponent<Animator>();
        _character = GetComponent<PlayerStateMachine>();

        _XAxis = Animator.StringToHash("XAxis");
        _YAxis = Animator.StringToHash("YAxis");
        _DashingAnim = Animator.StringToHash("IsDashing");
        _JumpAnim = Animator.StringToHash("IsJumping");
        _FallAnim = Animator.StringToHash("IsFalling");
        _LandAnim = Animator.StringToHash("IsLanded");
        _AttackAnim = Animator.StringToHash("IsAttack");
        _RunAnim = Animator.StringToHash("IsRunning");
    }

    private void Update()
    {
        DoWalkAnim();
        DoDashAnim();
        DoRunAnim();
        DoJumpAnim();
        DoFallAnim();
        DoLandAnim();
        DoAttackAnim();
    }

    private void DoWalkAnim()
    {
        LerpVelocityies();
        _anim.SetFloat(_XAxis, _XVelocity);
        _anim.SetFloat(_YAxis, _YVelocity);
    }

    private void LerpVelocityies()
    {
        _XVelocity = Mathf.Lerp(_XVelocity, _character.CurrentMovementInputX, .05f);
        _YVelocity = Mathf.Lerp(_YVelocity, _character.CurrentMovementInputY, .05f);
    }

    private void DoRunAnim() => _anim.SetBool(_RunAnim, _character.IsRunning);
    private void DoJumpAnim() => _anim.SetBool(_JumpAnim, _character.IsJumping);
    private void DoDashAnim() => _anim.SetBool(_DashingAnim, _character.IsDashing);
    private void DoAttackAnim() => _anim.SetBool(_AttackAnim, _character.IsAttacking);
    private void DoFallAnim() => _anim.SetBool(_FallAnim, !_character.Controller.isGrounded);
    private void DoLandAnim() => _anim.SetBool(_LandAnim, _character.Controller.isGrounded);
}
