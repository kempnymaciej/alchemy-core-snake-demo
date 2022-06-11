using UnityEngine;

namespace AlchemyBow.CoreDemos.Views
{
    /// <summary>
    /// Represents a base view.
    /// </summary>
    public class View : MonoBehaviour
    {
        /// <summary>
        /// Activates/Deactivates the view, depending on the given <c>true</c> or <c>false</c> value.
        /// </summary>
        /// <param name="value">If <c>true</c> activates the view; otherwise, deactivates it.</param>
        public virtual void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }
    } 
}
