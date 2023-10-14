using UnityEngine;
using Zenject;
using Game.Configs;

namespace Game.Installers
{
    public class ConfigInstaller : MonoInstaller
    {
        [SerializeField] private HeroConfig _heroConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(_heroConfig);
        }
    }
}
