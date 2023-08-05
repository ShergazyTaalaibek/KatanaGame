using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadIKAnim : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private bool _ikActive = false;
    [SerializeField] private Transform _lookObj = null;
    private int _headLayer;

    public void Initialize()
    {
        animator = GetComponent<Animator>();
    }

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