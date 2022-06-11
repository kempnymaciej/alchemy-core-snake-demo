using AlchemyBow.Core.IoC;
using AlchemyBow.Core.States;
using AlchemyBow.CoreDemos.ViewControllers;

namespace AlchemyBow.CoreDemos.Core.Elements
{
    // This is a child state of `GameState`. It is active while the game is paused.
    [InjectionTarget]
    public class GamePauseState : IState
    {
        [Inject]
        private GamePauseViewController gamePauseViewController;

        public void Enter()
        {
            gamePauseViewController.SetActive(true);
        }

        public void Exit()
        {
            gamePauseViewController.SetActive(false);
        }

        // Override ToString () for better debug printing.
        public override string ToString()
        {
            return GetType().Name;
        }
    }
}
