using AlchemyBow.Core.IoC;
using AlchemyBow.CoreDemos.Core;
using AlchemyBow.CoreDemos.Views;

namespace AlchemyBow.CoreDemos.ViewControllers
{
    // This controller operates on the game pause view and it is relevant for the game scene.
    [InjectionTarget]
    public class GamePauseViewController
    {
        [Inject]
        private readonly GamePauseView pauseView;
        [Inject]
        private readonly PauseSwitchingCondition pauseCondition;
        [Inject]
        private readonly MenuTrigger menuTrigger;

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
                pauseView.SetActive(active);
                if (active)
                {
                    pauseView.AddResumeClickListener(OnResumeClick);
                    pauseView.AddMenuClickListener(OnMenuClick);
                }
                else
                {
                    pauseView.RemoveResumeClickListener(OnResumeClick);
                    pauseView.RemoveMenuClickListener(OnMenuClick);
                }
            }
        }

        private void OnResumeClick()
        {
            pauseCondition.Trigger();
        }

        private void OnMenuClick()
        {
            menuTrigger.Trigger();
        }
    } 
}
