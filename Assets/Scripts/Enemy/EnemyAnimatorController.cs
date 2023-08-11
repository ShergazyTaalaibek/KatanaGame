using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    private Animator _anim;
    private EnemyStateMachine _character;
    private int _XAxis, _YAxis, _JumpAnim, _FallAnim, _LandAnim, _AttackAnim;
    private float _XVelocity = 0;
    private float _YVelocity = 0;

    public void Initialize()
    {
        _anim = GetComponent<Animator>();
        _character = GetComponent<EnemyStateMachine>();

        _XAxis = Animator.StringToHash("XAxis");
        _YAxis = Animator.StringToHash("YAxis");
        _JumpAnim = Animator.StringToHash("IsJumping");
        _FallAnim = Animator.StringToHash("IsFalling");
        _LandAnim = Animator.StringToHash("IsLanded");
        _AttackAnim = Animator.StringToHash("IsAttack");
    }

    private void Update()
    {
        DoWalkAnim();
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
        _XVelocity = Mathf.Lerp(_XVelocity, _character.MovementInput.x, .05f);
        _YVelocity = Mathf.Lerp(_YVelocity, _character.MovementInput.z, .05f);
    }

    private void DoJumpAnim() => _anim.SetBool(_JumpAnim, _character.IsJumping);
    private void DoAttackAnim() => _anim.SetBool(_AttackAnim, _character.IsAttacking);
    private void DoFallAnim() => _anim.SetBool(_FallAnim, !_character.Controller.isGrounded);
    private void DoLandAnim() => _anim.SetBool(_LandAnim, _character.Controller.isGrounded);
}
