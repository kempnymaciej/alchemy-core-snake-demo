using System.Collections.Generic;
using UnityEngine;

namespace AlchemyBow.CoreDemos.Systems
{
    /// <summary>
    /// Represents a model of the classic snake game.
    /// </summary>
    public sealed class SnakeSystem
    {
        public enum Status { InProgress, Failed, Succeded }

        public delegate void SnakedMovedEventHandler(bool foodConsumed);
        public delegate void GameFinishedEventHandler(bool succeed);
        /// <summary>
        /// An event that is triggered when the snake is moved.
        /// </summary>
        public event SnakedMovedEventHandler SnakeMoved;
        /// <summary>
        /// An event that is triggered when the game is finished.
        /// </summary>
        public event GameFinishedEventHandler GameFinished;

        private readonly Vector2Int mapSize;
        private readonly Vector2Int initialPosition;
        private readonly List<Vector2Int> snake;
        private readonly bool[,] snakeOnMap;
        private Vector2Int? foodPosition;
        private Vector2Int? previousDirection;

        private Status currentStatus;

        /// <summary>
        /// Create a model fore the classic snake game.
        /// </summary>
        /// <param name="mapSize">The map size.</param>
        /// <param name="initialPosition">The start position.</param>
        public SnakeSystem(Vector2Int mapSize, Vector2Int initialPosition)
        {
            this.mapSize = mapSize;
            this.initialPosition = initialPosition;
            snake = new List<Vector2Int>();
            snakeOnMap = new bool[mapSize.x, mapSize.y];
            Reset();
        }

        /// <summary>
        /// Returns the current position of the food is any.
        /// </summary>
        /// <remarks>No food if the game is won.</remarks>
        public Vector2Int? FoodPosition => foodPosition;
        /// <summary>
        /// Returns the previous move direction is any.
        /// </summary>
        public Vector2Int? PreviousDirection => previousDirection;
        /// <summary>
        /// Returns the snake as a list of positions (with a head at [0]). 
        /// </summary>
        public IReadOnlyList<Vector2Int> Snake => snake;
        /// <summary>
        /// Returns the map size.
        /// </summary>
        public Vector2Int MapSize => mapSize;
        /// <summary>
        /// Returns the current status of the game.
        /// </summary>
        public Status CurrentStatus => currentStatus;

        /// <summary>
        /// Resets the game.
        /// </summary>
        public void Reset()
        {
            currentStatus = Status.InProgress;
            foreach (var snakeSegment in snake)
            {
                snakeOnMap[snakeSegment.x, snakeSegment.y] = false;
            }
            snake.Clear();
            snake.Add(initialPosition);
            snakeOnMap[initialPosition.x, initialPosition.y] = true;
            foodPosition = GetNextFoodPosition();
            previousDirection = null;
        }

        /// <summary>
        /// Moves the snake.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <remarks>The direction is clamped.</remarks>
        public void Move(Vector2Int direction)
        {
            if (currentStatus == Status.InProgress && (direction.x != 0 || direction.y != 0))
            {
                direction = ClampDirection(direction);
                previousDirection = direction;
                var headPosition = CalculateNewHeadPosition(direction);

                if (snakeOnMap[headPosition.x, headPosition.y])
                {
                    currentStatus = Status.Failed;
                    GameFinished?.Invoke(false);
                }
                else
                {
                    bool foodConsumed = headPosition == foodPosition;
                    MoveSnake(headPosition, foodConsumed);
                    if (foodConsumed)
                    {
                        if (foodPosition == null)
                        {
                            currentStatus = Status.Succeded;
                            GameFinished?.Invoke(true);
                        }
                    }
                }
            }
        }

        private Vector2Int CalculateNewHeadPosition(Vector2Int clampedDirection)
        {
            var result = snake[0] + clampedDirection;
            if (result.x < 0)
            {
                result.x = mapSize.x - 1;
            }
            else if (result.x >= mapSize.x)
            {
                result.x = 0;
            }
            if (result.y < 0)
            {
                result.y = mapSize.y - 1;
            }
            else if (result.y >= mapSize.y)
            {
                result.y = 0;
            }
            return result;
        }

        /// <summary>
        /// Clamps the direction so that it is valid for the game model.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public Vector2Int ClampDirection(Vector2Int direction)
        {
            if (direction.x != 0)
            {
                direction.x = direction.x > 0 ? 1 : -1;
                direction.y = 0;
            }
            else if (direction.y != 0)
            {
                direction.y = direction.y > 0 ? 1 : -1;
            }

            return direction;
        }

        private void MoveSnake(Vector2Int headPosition, bool foodConsumed)
        {
            if (foodConsumed)
            {
                snake.Insert(0, headPosition);
            }
            else
            {
                int lastSegmentIndex = snake.Count - 1;
                var lostSegment = snake[lastSegmentIndex];
                snakeOnMap[lostSegment.x, lostSegment.y] = false;
                for (int i = lastSegmentIndex; i > 0; i--)
                {
                    snake[i] = snake[i - 1];
                }
                snake[0] = headPosition;
            }
            snakeOnMap[headPosition.x, headPosition.y] = true;

            if (foodConsumed)
            {
                foodPosition = GetNextFoodPosition();
            }
            SnakeMoved?.Invoke(foodConsumed);
        }

        private Vector2Int? GetNextFoodPosition()
        {
            int totalMapSize = mapSize.x * mapSize.y;
            int rand = Random.Range(0, totalMapSize - snake.Count);

            for (int x = 0; x < mapSize.x; x++)
            {
                for (int y = 0; y < mapSize.y; y++)
                {
                    if (!snakeOnMap[x, y])
                    {
                        if (rand == 0)
                        {
                            return new Vector2Int(x, y);
                        }
                        rand--;
                    }
                }
            }
            return null;
        }
    }
}
