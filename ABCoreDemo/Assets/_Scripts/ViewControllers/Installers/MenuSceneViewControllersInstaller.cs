using AlchemyBow.Core;
using AlchemyBow.Core.IoC;

namespace AlchemyBow.CoreDemos.ViewControllers
{
    // This installer installs controllers of the menu scene views.
    // The are controllers independent and uses ICoreLoadingCallbacksHandler.
    // See `GameSceneViewControllersInstaller` for an example with usage
    // of direct references.
    public class MenuSceneViewControllersInstaller : MonoInstaller
    {
        public override void InstallBindings(IBindOnlyContainer container)
        {
            AddToLoadingCallbacksAndBind(container, new MainMenuViewController());
            AddToLoadingCallbacksAndBind(container, new MenuHintViewController());
        }

        private void AddToLoadingCallbacksAndBind(IBindOnlyContainer container, ICoreLoadingCallbacksHandler handler)
        {
            // If you bind to the dynamic list of ICoreLoadingCallbacksHandler,
            // the instance will receive the callbacks.
            container.AddToDynamicListBinding<ICoreLoadingCallbacksHandler>(handler);
            container.BindInaccessible(handler);
        }
    }
}
