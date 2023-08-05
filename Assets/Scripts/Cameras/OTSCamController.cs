using UnityEngine;

public class OTSCamController : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _activeDistances = 10;
    [SerializeField] private GameObject _otsCam;
    private float _distance;

    private void Update()
    {
        _otsCam.SetActive(IsEnoughDistance());
    }

    private float GetDistance()
    {
        _distance = Vector3.Distance(_playerTransform.position, _targetTransform.position);
        return _distance;
    }

    private bool IsEnoughDistance()
    {
        if (GetDistance() > _activeDistances)
            return true;
        else
            return false;
    }
}
