using UnityEngine;

namespace AlchemyBow.CoreDemos.Systems
{
    /// <summary>
    /// Allows you to read player input.
    /// </summary>
    public sealed class InputSystem : MonoBehaviour
    {
        /// <summary>
        /// An event that is triggered when the direction is changed.
        /// </summary>
        public event System.Action DirectionChanged;
        /// <summary>
        /// An event that is triggered when the pause button is pressed.
        /// </summary>
        public event System.Action PauseClicked;

        [SerializeField]
        private KeyCode leftKey = KeyCode.A;
        [SerializeField]
        private KeyCode rightKey = KeyCode.D;
        [SerializeField]
        private KeyCode downKey = KeyCode.S;
        [SerializeField]
        private KeyCode upKey = KeyCode.W;
        [SerializeField]
        private KeyCode pauseKey = KeyCode.Escape;

        private Vector2Int direction;

        public KeyCode LeftKey => leftKey;
        public KeyCode RightKey => rightKey;
        public KeyCode DownKey => downKey;
        public KeyCode UpKey => upKey;
        public KeyCode PauseKey => pauseKey;
        public Vector2Int Direction => direction;

        /// <summary>
        /// Activates/Deactivates the system, depending on the given <c>true</c> or <c>false</c> value.
        /// </summary>
        /// <param name="value">If <c>true</c> activates the system; otherwise, deactivates it.</param>
        public void SetActive(bool value)
        {
            if (enabled != value)
            {
                if (value)
                {
                    UpdateDirection();
                }
                else if (direction != Vector2Int.zero)
                {
                    direction = Vector2Int.zero;
                    DirectionChanged?.Invoke();
                }
                enabled = value; 
            }
        }

        private void UpdateDirection()
        {
            var previousDirection = direction;
            direction = Vector2Int.zero;
            if (Input.GetKey(leftKey))
            {
                direction.x -= 1;
            }
            if (Input.GetKey(rightKey))
            {
                direction.x += 1;
            }
            if (Input.GetKey(downKey))
            {
                direction.y -= 1;
            }
            if (Input.GetKey(upKey))
            {
                direction.y += 1;
            }
            if (previousDirection != direction)
            {
                DirectionChanged?.Invoke();
            }
        }

        private void Update()
        {
            UpdateDirection();
            if (Input.GetKeyDown(pauseKey))
            {
                PauseClicked?.Invoke();
            }
        }

        private void Reset()
        {
            enabled = false;
        }
    } 
}
