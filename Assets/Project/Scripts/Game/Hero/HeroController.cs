using UnityEngine;
using Game.Configs;
using Zenject;

namespace Game
{
    public class HeroController : MonoBehaviour
    {
        private HeroConfig _heroConfig;

        // State Machine
        

        [Inject]
        private void Construct(HeroConfig heroConfig)
        {
            _heroConfig = heroConfig;
        }

        private void Update()
        {
            //_currentState.UpdateStates();
        }
    }
}
