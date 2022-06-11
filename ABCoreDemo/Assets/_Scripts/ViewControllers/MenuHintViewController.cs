using AlchemyBow.Core;
using AlchemyBow.Core.IoC;
using AlchemyBow.CoreDemos.Systems;
using AlchemyBow.CoreDemos.Views;

namespace AlchemyBow.CoreDemos.ViewControllers
{
    // This controller uses the hint view to show the AlchemyBow.Core package version.
    // It is relevant for the main menu scene and uses `ICoreLoadingCallbacksHandler`.
    [InjectionTarget]
    public class MenuHintViewController : ICoreLoadingCallbacksHandler
    {
        [Inject]
        private readonly CoreVersionSystem coreVersionSystem;
        [Inject]
        private readonly HintView hintView;

        public void OnCoreLoadingFinished()
        {
            hintView.SetActive(true);
            var version = coreVersionSystem.Version;
            if (version != null)
            {
                hintView.SetHind($"The latest version of the AlchemyBow.Core package is: {version}");
            }
            else
            {
                hintView.SetHind($"Failed to get tags from `{CoreVersionSystem.TagsUrl}`.");
            }
        }

        public void OnCoreSceneChangeStarted()
        {
            hintView.SetActive(false);
        }
    } 
}
