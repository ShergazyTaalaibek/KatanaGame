using UnityEngine;

public class CamManager : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _activeDistances = 30;
    [SerializeField] private float _notActiveDistances = 10;
    [SerializeField] private bool _reverse;
    [SerializeField] private GameObject _virtualCamera;
    private float _distance;

    private void Update()
    {
        if (_reverse)
            SetReverseActiveCamera(_virtualCamera);
        else
            SetActiveCamera(_virtualCamera);
    }

    private void SetActiveCamera(GameObject camera)
    {
        if (GetDistance() < _notActiveDistances)
            camera.SetActive(false);
        else if (GetDistance() > _activeDistances)
            camera.SetActive(true);
    }

    private void SetReverseActiveCamera(GameObject camera)
    {
        if (GetDistance() > _notActiveDistances)
            camera.SetActive(false);
        else if (GetDistance() < _activeDistances)
            camera.SetActive(true);
    }

    private float GetDistance()
    {
        _distance = Vector3.Distance(_playerTransform.position, _targetTransform.position);
        return _distance;
    }
}
