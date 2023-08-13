using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatSystem : MonoBehaviour
{
    [SerializeField] private List<AttackScriptableObject> _attackChain;
    [SerializeField] private int _attackChainIndex;
    private Animator _animator;

    public void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public float GetAnimationLength()
    {
        return _attackChain[_attackChainIndex]._animationClip.length;
    }

    public void IterateChain()
    {
        if (_attackChainIndex == _attackChain.Count - 1)
            _attackChainIndex = 0;
        else
            _attackChainIndex++;
        _animator.runtimeAnimatorController = _attackChain[_attackChainIndex]._animatorOV;
    }
}
