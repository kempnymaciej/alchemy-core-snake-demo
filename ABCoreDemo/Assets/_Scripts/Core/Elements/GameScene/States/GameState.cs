using AlchemyBow.Core.IoC;
using AlchemyBow.Core.States;
using AlchemyBow.CoreDemos.Avatars;
using AlchemyBow.CoreDemos.Controllers;
using AlchemyBow.CoreDemos.Systems;
using AlchemyBow.CoreDemos.ViewControllers;
using UnityEngine;

namespace AlchemyBow.CoreDemos.Core.Elements
{
    // This is a root state of the `GameSceneCoreController`.
    // In general, it works similar to `MenuSceneCoreController`,
    // but it extends the logic with player input and
    // delegates some tasks to sub-states to introduce a pause.
    [InjectionTarget]
    public class GameState : IState
    {
        [Inject]
        private readonly PauseSwitchingCondition pauseCondition;
        [Inject]
        private readonly SnakeSystem snakeSystem;
        [Inject]
        private readonly AudioSystem audioSystem;
        [Inject]
        private readonly InputSystem inputSystem;
        [Inject]
        private readonly ScoreSystem scoreSystem;
        [Inject]
        private readonly TickSystem tickSystem;
        [Inject]
        private readonly SnakeObserverCameraController snakeCameraController;
        [Inject]
        private readonly GameResultViewController gameResultViewController;
        [Inject]
        private readonly GameHintViewController gameHintViewController;
        [Inject]
        private readonly SnakeAvatar snakeAvatar;
        [Inject]
        private readonly FoodAvatar foodAvatar;
        [Inject]
        private readonly FloorAvatar floorAvatar;
        [Inject]
        private readonly SnakeAudioClipSet audioClipSet;

        private bool gameFinished;

        public void Enter()
        {
            inputSystem.SetActive(true);
            snakeCameraController.SetActive(true);
            gameResultViewController.SetActive(true);
            gameHintViewController.SetActive(true);

            inputSystem.PauseClicked += OnPauseClicked;
            snakeSystem.SnakeMoved += OnSnakeMoved;
            snakeSystem.GameFinished += OnSnakeGameFinished;
            UpdateSnakeFood();
            floorAvatar.SetFloor(Vector2Int.zero, snakeSystem.MapSize);
            snakeAvatar.SetSegments(snakeSystem.Snake);
        }

        public void Exit()
        {
            inputSystem.SetActive(false);
            snakeCameraController.SetActive(false);
            gameResultViewController.SetActive(false);
            gameHintViewController.SetActive(false);

            inputSystem.PauseClicked -= OnPauseClicked;
            snakeSystem.SnakeMoved -= OnSnakeMoved;
            snakeSystem.GameFinished -= OnSnakeGameFinished;
            foodAvatar.SetFoodActive(false);
            floorAvatar.ClearFloor();
            snakeAvatar.ClearSegments();
        }

        private void UpdateSnakeFood()
        {
            var foodPosition = snakeSystem.FoodPosition;
            if (foodPosition == null)
            {
                foodAvatar.SetFoodActive(false);
            }
            else
            {
                foodAvatar.SetFoodActive(true);
                foodAvatar.SetFoodPosition((Vector2Int)foodPosition);
            }
        }

        #region Callbacks
        private void OnPauseClicked()
        {
            if (!gameFinished)
            {
                pauseCondition.Trigger(); 
            }
        }

        private void OnSnakeMoved(bool foodConsumed)
        {
            snakeAvatar.SetSegments(snakeSystem.Snake);
            if (foodConsumed)
            {
                UpdateSnakeFood();
                audioSystem.PlayEffect(audioClipSet.Eat);
            }
            else
            {
                audioSystem.PlayEffect(audioClipSet.Move);
            }
        }

        private void OnSnakeGameFinished(bool succeed)
        {
            gameFinished = true;
            if (!succeed)
            {
                audioSystem.PlayEffect(audioClipSet.Failure);
            }
            scoreSystem.CheckAndUpdateBestScore(snakeSystem.Snake.Count, tickSystem.TickIndex);
        }
        #endregion

        // Override ToString () for better debug printing.
        public override string ToString()
        {
            return GetType().Name;
        }
    }
}
