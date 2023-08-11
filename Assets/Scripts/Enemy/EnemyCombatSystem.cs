using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatSystem : MonoBehaviour
{
    [SerializeField] private List<AttackScriptableObject> _attackChain;
    [SerializeField] private float _attackDuration;
    [SerializeField] private int _attackChainIndex;
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            _animator.runtimeAnimatorController = _attackChain[_attackChainIndex]._animatorOV;
            if (_attackChainIndex == _attackChain.Count)
                _attackChainIndex = 0;
            else
                _attackChainIndex++;
        }
    }
}
