using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private PlayerStateMachine _stateMachine;
    [SerializeField] private MovePlayer _movePlayer;

    private void Awake()
    {
        _stateMachine.Initialize();
        _movePlayer?.Initialize();
    }
}
