using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform _lookAtTarget;
    private Transform _enemyTransform;
    [SerializeField] private float _rotatingSpeed;

    public void Initialize()
    {
        _enemyTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        ApplyRotation();
    }

    public void ApplyRotation()
    {
        Vector3 targetPostition = new Vector3(_lookAtTarget.position.x,
                                       _enemyTransform.position.y,
                                       _lookAtTarget.position.z);
        var targetRotation = Quaternion.LookRotation(targetPostition - _enemyTransform.position);
        _enemyTransform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotatingSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((layerMask.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            Debug.Log("Sword hit");
        }
    }
}
