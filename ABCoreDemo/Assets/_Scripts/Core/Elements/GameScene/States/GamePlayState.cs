using AlchemyBow.Core.IoC;
using AlchemyBow.Core.States;
using AlchemyBow.CoreDemos.Systems;
using UnityEngine;

namespace AlchemyBow.CoreDemos.Core.Elements
{
    // This is a child state of `GameState`. It is active while the game is not paused.
    [InjectionTarget]
    public class GamePlayState : IState
    {
        [Inject]
        private readonly SnakeSystem snakeSystem;
        [Inject]
        private readonly InputSystem inputSystem;
        [Inject]
        private readonly TickSystem tickSystem;

        private Vector2Int direction;

        public void Enter()
        {
            tickSystem.SetActive(true);
            inputSystem.DirectionChanged += OnDirectionChanged;
            tickSystem.Ticked += OnTicked;
        }

        public void Exit()
        {
            tickSystem.Ticked -= OnTicked;
            inputSystem.DirectionChanged -= OnDirectionChanged;
            tickSystem.SetActive(false);
        }

        private void UpdateDirection()
        {
            var direction = inputSystem.Direction;
            if (direction.x != 0 || direction.y != 0)
            {
                this.direction = direction;
            }
        }

        private void OnDirectionChanged()
        {
            UpdateDirection();
        }

        private void OnTicked()
        {
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

        // Override ToString () for better debug printing.
        public override string ToString()
        {
            return GetType().Name;
        }
    }
}