using UnityEngine;

public class TiltCamController : MonoBehaviour
{
    [SerializeField] private GameObject _rightTiltCam, _leftTiltCam;
    [SerializeField] private CharacterStateMachine _character;

    private void Update()
    {
        SetCamActive(_rightTiltCam, _character.CurrentMovementInputX > .5);
        SetCamActive(_leftTiltCam, _character.CurrentMovementInputX < -.5);
    }

    private void SetCamActive(GameObject cam, bool b)
    {
        if (b)
            cam.SetActive(true);
        else
            cam.SetActive(false);
    }
}
