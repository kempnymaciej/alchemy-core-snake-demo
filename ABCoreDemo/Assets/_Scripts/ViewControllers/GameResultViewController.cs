using AlchemyBow.Core.IoC;
using AlchemyBow.CoreDemos.Core;
using AlchemyBow.CoreDemos.Systems;
using AlchemyBow.CoreDemos.Views;

namespace AlchemyBow.CoreDemos.ViewControllers
{
    // This controller operates on the game result view and it is relevant for the game scene.
    [InjectionTarget]
    public sealed class GameResultViewController
    {
        [Inject]
        private readonly GameResultView resultView;
        [Inject]
        private readonly SnakeSystem snakeSystem;
        [Inject]
        private readonly TickSystem tickSystem;
        [Inject]
        private readonly MenuTrigger menuTrigger;
        [Inject]
        private readonly GameTrigger gameTrigger;

        private bool active;

        /// <summary>
        /// Activates/Deactivates the controller, depending on the given <c>true</c> or <c>false</c> value.
        /// </summary>
        /// <param name="value">If <c>true</c> activates the controller; otherwise, deactivates it.</param>
        public void SetActive(bool value)
        {
            if (active != value)
            {
                active = value;
                if (active)
                {
                    snakeSystem.GameFinished += OnGameFinished;
                    resultView.AddResetClickListener(OnResetButtonClicked);
                    resultView.AddMenuClickListener(OnMenuButtonClicked);
                }
                else
                {
                    snakeSystem.GameFinished -= OnGameFinished;
                    resultView.RemoveResetClickListener(OnResetButtonClicked);
                    resultView.RemoveMenuClickListener(OnMenuButtonClicked);
                }
            }
        }

        #region Callbacks
        private void OnGameFinished(bool succeed)
        {
            resultView.SetMessage($"{snakeSystem.CurrentStatus.ToString().ToUpper()}\n Score: {snakeSystem.Snake.Count} / {tickSystem.TickIndex}");
            resultView.SetActive(true);
        }

        private void OnResetButtonClicked()
        {
            resultView.SetActive(false);
            gameTrigger.Trigger();
        }

        private void OnMenuButtonClicked()
        {
            resultView.SetActive(false);
            menuTrigger.Trigger();
        } 
        #endregion
    } 
}
