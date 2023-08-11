using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _timeSpeed = 1;

    void Start()
    {
        
    }

    void Update()
    {
        Time.timeScale = _timeSpeed;
    }
}
