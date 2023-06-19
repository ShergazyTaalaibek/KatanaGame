using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    private Transform _playerTransform;
    [SerializeField] private Transform _target;
    private Transform _cameraTransform;

    public void Initialize()
    {
        _playerTransform = GetComponent<Transform>();
        _cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        //Vector3 targetPosition = new Vector3(_target.position.x, _playerTransform.position.y, _target.position.z);
        //_playerTransform.LookAt(targetPosition);

        _playerTransform.rotation = Quaternion.Euler(0, _cameraTransform.rotation.eulerAngles.y, 0);
    }
}
