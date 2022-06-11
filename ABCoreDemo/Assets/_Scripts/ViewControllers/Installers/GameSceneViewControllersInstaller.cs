using AlchemyBow.Core.IoC;

namespace AlchemyBow.CoreDemos.ViewControllers
{
    // This installer installs controllers of the game scene views.
    // The controllers are designed to be used directly with references.
    // See `MenuSceneViewControllersInstaller` for an example with usage
    // of ICoreLoadingCallbacksHandler.
    public sealed class GameSceneViewControllersInstaller : MonoInstaller
    {
        public override void InstallBindings(IBindOnlyContainer container)
        {
            container.Bind(new GamePauseViewController());
            container.Bind(new GameResultViewController());
            container.Bind(new GameHintViewController());
        }
    }
}
