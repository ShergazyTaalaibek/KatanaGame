using UnityEngine;

namespace Game.Setup
{
    public class GameSetup : MonoBehaviour
    {
        [SerializeField] private int targetFPS = 60;

        private void Awake()
        {
            Application.targetFrameRate = targetFPS;
        }
    }
}