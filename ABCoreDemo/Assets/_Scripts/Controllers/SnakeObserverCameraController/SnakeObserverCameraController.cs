using AlchemyBow.Core.IoC;
using AlchemyBow.CoreDemos.Systems;
using UnityEngine;

namespace AlchemyBow.CoreDemos.Controllers
{
    /// <summary>
    /// Controls the camera to observe the snake.
    /// </summary>
    [InjectionTarget]
    public sealed class SnakeObserverCameraController : MonoBehaviour
    {
        [Inject]
        private readonly SnakeSystem snakeSystem;

        [SerializeField, Min(.1f)]
        private float lerpSpeed = 1;
        [SerializeField]
        private Camera targetCamera;

        private Quaternion targetRotation;
        private bool active;

        /// <summary>
        /// Activates/Deactivates the controller, depending on the given <c>true</c> or <c>false</c> value.
        /// </summary>
        /// <param name="value">If <c>true</c> activates the controller; otherwise, deactivates it.</param>
        /// <remarks>Controls the camera game object and its components.</remarks>
        public void SetActive(bool value)
        {
            if (active != value)
            {
                active = value;
                gameObject.SetActive(value);
                if (value)
                {
                    Vector3 position = transform.position;
                    position.x = snakeSystem.MapSize.x / 2f - .5f;
                    position.y = snakeSystem.MapSize.y;
                    position.z = snakeSystem.MapSize.y / 2f - .5f;
                    transform.position = position;
                    LookAt(snakeSystem.Snake[0]);
                    snakeSystem.SnakeMoved += OnSnakeMoved;
                }
                else
                {
                    snakeSystem.SnakeMoved -= OnSnakeMoved;
                }
            }
        }

        private void OnSnakeMoved(bool foodConsumed)
        {
            InterpolateLookAt(snakeSystem.Snake[0]);
        }

        private void LookAt(Vector2Int targetPosition)
        {
            SetTargetRotation(targetPosition);
            transform.rotation = targetRotation;
            enabled = false;
        }

        private void InterpolateLookAt(Vector2Int targetPosition)
        {
            SetTargetRotation(targetPosition);
            enabled = true;
        }

        private void SetTargetRotation(Vector2Int targetPosition)
        {
            var lookAtPosition = new Vector3(targetPosition.x, 0, targetPosition.y);
            targetRotation = Quaternion.LookRotation(lookAtPosition - transform.position, Vector3.forward);
        }

        private void Update()
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, lerpSpeed * Time.deltaTime);
        }

        private void Reset()
        {
            gameObject.SetActive(false);
            enabled = false;
        }
    } 
}
