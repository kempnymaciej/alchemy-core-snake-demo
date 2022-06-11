using AlchemyBow.Core.IoC;
using AlchemyBow.CoreDemos.Systems;
using AlchemyBow.CoreDemos.Views;

namespace AlchemyBow.CoreDemos.ViewControllers
{
    // This controller operates on the hint view and it is relevant for the game scene.
    [InjectionTarget]
    public sealed class GameHintViewController
    {
        [Inject]
        private readonly InputSystem inputSystem;
        [Inject]
        private readonly HintView hintView;

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
                    hintView.SetActive(true);
                    hintView.SetHind($"Use `{inputSystem.UpKey}`,`{inputSystem.DownKey}`,`{inputSystem.LeftKey}` and `{inputSystem.RightKey}` to control the snake. Click `{inputSystem.PauseKey}` to toggle pause.");
                    inputSystem.DirectionChanged += OnInputDirectionChanged;
                }
                else
                {
                    active = false;
                    hintView.SetActive(false);
                    inputSystem.DirectionChanged -= OnInputDirectionChanged;
                }
            }
        }

        private void OnInputDirectionChanged()
        {
            SetActive(false);
        }
    }
}
