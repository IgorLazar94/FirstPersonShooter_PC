using MenuScene;
using Zenject;
using UnityEngine;

namespace ZenjectScripts
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private GameObject localizationControllerPrefab;

        public override void InstallBindings()
        {
            Container.Bind<LocalizationController>().FromComponentInNewPrefab(localizationControllerPrefab).AsSingle().NonLazy();
        }
    }
}
