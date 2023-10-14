using UnityEngine;
using UnityEngine.UI;

public class StaminaSlider : MonoBehaviour
{
    private Slider _slider;
    private CanvasGroup _canvasGroup;
    private Transform _transform;
    private Transform _mainCamTransform;
    [SerializeField] private float _fadingSpeed = 100;

    public void Initialize()
    {
        _transform = GetComponent<Transform>();
        _mainCamTransform = Camera.main.GetComponent<Transform>();
        _slider = GetComponentInChildren<Slider>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        LookAtCam();
        Fade(_slider.value >= _slider.maxValue);
    }

    public void SetMaxValue(float value)
    {
        _slider.maxValue = value;
        SetValue(value);
    }

    private void LookAtCam()
    {
        _transform.LookAt(_mainCamTransform);
    }

    public void SetValue(float value)
    {
        _slider.value = value;
    }

    private void Fade(bool b)
    {
        if (b)
            _canvasGroup.alpha = Mathf.Lerp(_canvasGroup.alpha, 0, Time.deltaTime * _fadingSpeed);
        else
            _canvasGroup.alpha = Mathf.Lerp(_canvasGroup.alpha, 1, Time.deltaTime * _fadingSpeed);
    }
}
