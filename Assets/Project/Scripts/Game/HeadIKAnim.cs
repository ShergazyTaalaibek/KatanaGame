using UnityEngine;

public class HeadIKAnim : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private bool _ikActive = true;
    [SerializeField] private Transform _lookObj = null;

    public void Initialize()
    {
        animator = GetComponent<Animator>();
    }

    public void SetHeadIK(bool b) => _ikActive = b;

    private void OnAnimatorIK()
    {
        if (animator)
        {
            if (_ikActive)
            {
                if (_lookObj != null)
                {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(_lookObj.position);
                }
            }
            else
            {
                animator.SetLookAtWeight(0);
            }
        }
    }
}
