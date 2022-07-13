using AlchemyBow.Core;
using AlchemyBow.Core.IoC;
using AlchemyBow.CoreDemos.Avatars;
using AlchemyBow.CoreDemos.Controllers;
using AlchemyBow.CoreDemos.Systems;
using System.Collections.Generic;
using UnityEngine;

namespace AlchemyBow.CoreDemos.Core.Elements
{
    // This is a core controller for the main menu scene.
    // In this example, all logic is performed directly here.
    // Go to `GameSceneCoreController` for an example where
    // logic is delegated to states.
    [InjectionTarget]
    public sealed class MenuSceneCoreController : SnakeGameCoreController
    {
        [Inject]
        private readonly SnakeObserverCameraController snakeObserverCameraController;
        [Inject]
        private readonly SnakeSystem snakeSystem;
        [Inject]
        private readonly TickSystem tickSystem;
        [Inject]
        private readonly CoreVersionSystem coreVersionSystem;

        [Inject]
        private readonly SnakeAvatar snakeAvatar;
        [Inject]
        private readonly FoodAvatar foodAvatar;
        [Inject]
        private readonly FloorAvatar floorAvatar;

        [Inject]
        private readonly GameTrigger gameTrigger;

        // You can add some extra delay after loading.
        protected override float PostLoadingDelay => .5f;

        protected override IEnumerable<ICoreLoadable> GetLoadables()
        {
            // Note that the loadables loaded here (in the core controller)
            // are loaded every time the related scene is loaded.
            Debug.LogFormat($"<color=#cc33ff>{GetType().Name}:</color> GetLoadables()");
            yield return coreVersionSystem;

            // Here are some additional loadables to show how they are loaded in order.
            yield return new SampleLoadable("Example1", 400);
            yield return new SampleLoadable("Example2", 400);
        }

        protected override void OnLoadingFinished()
        {
            // Subscribe the relevant scene change trigger:
            gameTrigger.Triggered += ChangeToGameScene;

            base.OnLoadingFinished();

            // Initialize the game logic:
            snakeSystem.SnakeMoved += OnSnakeMoved;
            snakeSystem.GameFinished += OnGameFinished;
            tickSystem.Ticked += OnTicked;

            tickSystem.SetActive(true);
            snakeObserverCameraController.SetActive(true);

            UpdateSnakeFood();
            floorAvatar.SetFloor(Vector2Int.zero, snakeSystem.MapSize);
            snakeAvatar.SetSegments(snakeSystem.Snake);
        }

        protected override void OnSceneChangeStarted()
        {
            // Unsubscribe the relevant scene change trigger:
            gameTrigger.Triggered -= ChangeToGameScene;

            // Deinitialize the game logic:
            tickSystem.SetActive(false);

            snakeSystem.SnakeMoved -= OnSnakeMoved;
            snakeSystem.GameFinished -= OnGameFinished;
            tickSystem.Ticked -= OnTicked;

            foodAvatar.SetFoodActive(false);
            floorAvatar.ClearFloor();
            snakeAvatar.ClearSegments();

            snakeObserverCameraController.SetActive(false);
            base.OnSceneChangeStarted();
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
        private void OnSnakeMoved(bool foodConsumed)
        {
            snakeAvatar.SetSegments(snakeSystem.Snake);
            if (foodConsumed)
            {
                UpdateSnakeFood();
            }
        }

        private void OnGameFinished(bool succeed)
        {
            snakeSystem.Reset();
            snakeAvatar.SetSegments(snakeSystem.Snake);
            UpdateSnakeFood();
        }

        private void OnTicked()
        {
            var possibleFoodPosition = snakeSystem.FoodPosition;
            Vector2Int direction;
            if (possibleFoodPosition != null)
            {
                direction = (Vector2Int)possibleFoodPosition - snakeSystem.Snake[0];
            }
            else
            {
                direction = Vector2Int.zero;
            }

            var snakePreviousDirection = snakeSystem.PreviousDirection;
            if (snakeSystem.Snake.Count <= 1 
                || snakePreviousDirection == null
                || snakeSystem.ClampDirection(direction) != -(Vector2Int)snakePreviousDirection)
            {
                snakeSystem.Move(direction);
            }
            else
            {
                snakeSystem.Move((Vector2Int)snakePreviousDirection);
            }
        } 
        #endregion
    } 
}
