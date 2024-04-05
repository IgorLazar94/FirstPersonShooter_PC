using ModularFirstPersonController.FirstPersonController;
using PauseSystem;
using Zenject;

namespace ZenjectScripts
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // registration for class instances
            Container.Bind<PauseService>().AsSingle().NonLazy();
            Container.Bind<FirstPersonController>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<DynamicCanvasController>().FromComponentInHierarchy().AsSingle().NonLazy();
        }
    }
}
