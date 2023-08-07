using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private CharacterStateMachine _playerStateMachine;
    [SerializeField] private EnemyController _enemyController;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private HeadIKAnim _playerHeadIKAnim;

    private void Awake()
    {
        _playerStateMachine?.Initialize();
        _enemyController?.Initialize();
        _playerAnimator.Initialize();
        _playerHeadIKAnim?.Initialize();
    }
}
