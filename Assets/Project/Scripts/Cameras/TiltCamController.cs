using UnityEngine;
using Cinemachine;

public class TiltCamController : MonoBehaviour
{
    [SerializeField] private GameObject _rightTiltCam, _leftTiltCam;
    [SerializeField] private PlayerStateMachine _character;
    [SerializeField] private CinemachineVirtualCamera _cinemachineCamera;
    [SerializeField] private float _tiltingSpeed = 10;
    [SerializeField] private float _tiltingAngle = 1;

    private void Update()
    {
        //SetCamActive(_rightTiltCam, _character.CurrentMovementInputX > .5);
        //SetCamActive(_leftTiltCam, _character.CurrentMovementInputX < -.5);

        SetTilting();
    }

    private void TiltCamera(float tiltAngle)
    {
        _cinemachineCamera.m_Lens.Dutch = Mathf.Lerp(_cinemachineCamera.m_Lens.Dutch, tiltAngle, Time.deltaTime * _tiltingSpeed);
    }

    private void SetTilting()
    {
        if (_character.CurrentMovementInputX > 0.5)
            TiltCamera(-_tiltingAngle);
        else if (_character.CurrentMovementInputX < -0.5)
            TiltCamera(_tiltingAngle);
        else
            TiltCamera(0);
    }

    //private void SetCamActive(GameObject cam, bool b)
    //{
    //    if (b)
    //        cam.SetActive(true);
    //    else
    //        cam.SetActive(false);
    //}
}
