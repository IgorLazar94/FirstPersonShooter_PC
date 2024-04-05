using ModularFirstPersonController.FirstPersonController;
using PauseSystem;
using Zenject;
using UnityEngine;

namespace ZenjectScripts
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private SparksGameObject sparksFxPrefab;
        [SerializeField] private SlimeGameObject slimePrefab;
        [SerializeField] private BulletHoleGameObject playerBulletPrefab;



        public override void InstallBindings()
        {
            Container.Bind<PauseService>().AsSingle().NonLazy();
            Container.Bind<FirstPersonController>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<DynamicCanvasController>().FromComponentInHierarchy().AsSingle().NonLazy();
            
            BindPoolParticles();
        }

        private void BindPoolParticles()
        {
            Container.Bind<SparksGameObject>().FromComponentInNewPrefab(sparksFxPrefab).AsSingle();
            Container.Bind<SlimeGameObject>().FromComponentInNewPrefab(slimePrefab).AsSingle();
            Container.Bind<BulletHoleGameObject>().FromComponentInNewPrefab(playerBulletPrefab).AsSingle();
        }
    }
}
