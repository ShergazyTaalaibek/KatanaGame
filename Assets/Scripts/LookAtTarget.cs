using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _playerTransform;

    public void Initialize()
    {
        _playerTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        Vector3 targetPosition = new Vector3(_target.position.x, _playerTransform.position.y, _target.position.z);
        _playerTransform.LookAt(targetPosition);
    }
}
