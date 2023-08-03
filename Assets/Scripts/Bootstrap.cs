using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private CharacterStateMachine _playerStateMachine;
    [SerializeField] private MovePlayer _movePlayer;

    private void Awake()
    {
        _playerStateMachine?.Initialize();
        _movePlayer?.Initialize();
    }
}
