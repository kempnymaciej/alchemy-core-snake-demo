using UnityEngine;

namespace AlchemyBow.CoreDemos.Avatars
{
    /// <summary>
    /// Visualizes a single piece of food.
    /// </summary>
    public sealed class FoodAvatar : MonoBehaviour
    {
        [SerializeField]
        private float height = 0;

        /// <summary>
        /// Activates/Deactivates the avatar, depending on the given <c>true</c> or <c>false</c> value.
        /// </summary>
        /// <param name="value">If <c>true</c> activates the avatar; otherwise, deactivates it.</param>
        public void SetFoodActive(bool value)
        {
            gameObject.SetActive(value);
        }

        /// <summary>
        /// Sets the avatar position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <remarks>Converts (x,y) into (x, height, y).</remarks>
        public void SetFoodPosition(Vector2Int position)
        {
            transform.position = new Vector3(position.x, height, position.y);
        }

        private void Reset()
        {
            gameObject.SetActive(false);
        }
    } 
}
