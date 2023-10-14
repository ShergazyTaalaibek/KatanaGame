using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerStateMachine _playerStateMachine;
    [SerializeField] private EnemyStateMachine _enemyController;
    [SerializeField] private HeroAnimatorController _playerAnimator;
    [SerializeField] private EnemyAnimatorController _enemyAnimator;

    private void Awake()
    {
        _playerStateMachine?.Initialize();
        _enemyController?.Initialize();
        _playerAnimator?.Initialize();
        _enemyAnimator?.Initialize();
    }
}
