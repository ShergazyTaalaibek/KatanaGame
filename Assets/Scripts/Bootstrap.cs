using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private MovePlayer _movePlayer;
    [SerializeField] private LookAtTarget _lookAtTarget;

    private void Awake()
    {
        _movePlayer.Initialize();
        _lookAtTarget.Initialize();
    }
}
