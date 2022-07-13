using AlchemyBow.Core;
using AlchemyBow.Core.IoC;
using AlchemyBow.CoreDemos.Systems;
using UnityEngine;

namespace AlchemyBow.CoreDemos.Core.Elements
{
    // This is a base class for snake game core controllers.
    // I exists to avoid redundant code for common behaviors in the core controllers.
    [InjectionTarget]
    public abstract class SnakeGameCoreController : CoreController<SnakeGameProjectContext>
    {
        private const int MenuSceneBuildIndex = 0;
        private const int GameSceneBuildIndex = 1;

        [SerializeField]
        private AudioClipData musicClip;

        [Inject]
        private readonly AudioSystem audioSystem;

        protected override void OnStarted(OperationHandle operationHandle)
        {
            // The loading screen is activated (ensured) before anything else via a static interface.
            LoadingSceneUtility.EnsureLoadingSceneActive(true);
            LoadingSceneUtility.SetLoadingText("Loading ...");
            base.OnStarted(operationHandle);
        }

        protected override void OnBindingFinished()
        {
            base.OnBindingFinished();
            // The music is set before the loading stage.
            audioSystem.SetMusic(musicClip, false);
        }

        protected override void OnLoadablesProgressed(LoadablesProgress progress)
        {
            // You can use the progress to calculate the percentage progress.
            int percent = progress.loadableIndex + (progress.loadableCompleted ? 1 : 0);
            percent = (int)((percent / (float)progress.numberOfLoadables) * 100);
            LoadingSceneUtility.SetLoadingText($"Loading {percent}%");
            Debug.Log($"{progress.loadableIndex}/{progress.numberOfLoadables}: {progress.loadable} (complete={progress.loadableCompleted})");
        }

        protected override void OnLoadingFinished()
        {
            base.OnLoadingFinished();
            // The loading screen is deactivated at the end of the loading stage.
            LoadingSceneUtility.EnsureLoadingSceneActive(false);
        }

        protected override void OnSceneChangeStarted()
        {
            // Disables `EventSystem` to avoid loading problems.
            var eventSystem = UnityEngine.EventSystems.EventSystem.current;
            if (eventSystem != null)
            {
                eventSystem.enabled = false;
            }
            // The loading screen is activated at the end of the working stage.
            LoadingSceneUtility.EnsureLoadingSceneActive(true);
            base.OnSceneChangeStarted();
        }


        // A common method to go to the main menu scene.
        protected void ChangeToMenuScene()
        {
            ChangeScene(MenuSceneBuildIndex);
        }

        // A common method to go to the game scene.
        protected void ChangeToGameScene()
        {
            ChangeScene(GameSceneBuildIndex);
        }
    } 
}
