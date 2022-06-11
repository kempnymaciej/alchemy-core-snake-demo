using AlchemyBow.Core;
using AlchemyBow.Core.IoC;
using AlchemyBow.CoreDemos.Core;
using AlchemyBow.CoreDemos.Systems;
using AlchemyBow.CoreDemos.Views;
using UnityEngine;

namespace AlchemyBow.CoreDemos.ViewControllers
{
    // This controller operates on the main menu view.
    // It is relevant for the main menu scene and uses `ICoreLoadingCallbacksHandler`.
    [InjectionTarget]
    public sealed class MainMenuViewController : ICoreLoadingCallbacksHandler
    {
        [Inject]
        private readonly MainMenuView mainMenuView;
        [Inject]
        private readonly GameTrigger gameTrigger;
        [Inject]
        private readonly ScoreSystem scoreSystem;

        #region ICoreLoadingCallbacksHandler
        public void OnCoreLoadingFinished()
        {
            mainMenuView.SetActive(true);
            mainMenuView.AddPlayClickListener(OnPlayClick);
            mainMenuView.AddExitClickListener(OnExitClick);
            var score = scoreSystem.BestScore;
            if (score != null)
            {
                mainMenuView.SetBestScore($"{score.Points} / {score.Ticks}");
            }
            else
            {
                mainMenuView.SetBestScore("---");
            }
        }

        public void OnCoreSceneChangeStarted()
        {
            mainMenuView.SetActive(false);
            mainMenuView.RemovePlayClickListener(OnPlayClick);
            mainMenuView.RemoveExitClickListener(OnExitClick);
        } 
        #endregion

        private void OnPlayClick()
        {
            gameTrigger.Trigger();
        }

        private void OnExitClick()
        {
            if (Application.isEditor)
            {
                Debug.Log("The exit button only works in builds.");
            }
            else
            {
                Application.Quit();
            }
        }
    } 
}
