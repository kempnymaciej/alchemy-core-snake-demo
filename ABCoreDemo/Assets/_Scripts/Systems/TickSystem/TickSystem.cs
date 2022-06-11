using UnityEngine;

namespace AlchemyBow.CoreDemos.Systems
{
    /// <summary>
    /// Allows you to track time in intervals.
    /// </summary>
    public sealed class TickSystem : MonoBehaviour
    {
        /// <summary>
        /// An event that is triggered when the time interval ends.
        /// </summary>
        public event System.Action Ticked;

        [SerializeField, Min(.1f)]
        private float tickDuration = 1f;

        private float currentTickDuration;

        /// <summary>
        /// Return an index of the current time interval.
        /// </summary>
        public int TickIndex { get; private set; }

        /// <summary>
        /// Activates/Deactivates the system, depending on the given <c>true</c> or <c>false</c> value.
        /// </summary>
        /// <param name="value">If <c>true</c> activates the system; otherwise, deactivates it.</param>
        public void SetActive(bool value)
        {
            enabled = value;
        }

        /// <summary>
        /// Starts counting the current interval from the beginning.
        /// </summary>
        public void ResetCurrentTick()
        {
            currentTickDuration = 0;
        }

        private void Update()
        {
            currentTickDuration += Time.deltaTime;
            if (currentTickDuration >= tickDuration)
            {
                ResetCurrentTick();
                TickIndex++;
                Ticked?.Invoke();
            }
        }

        private void Reset()
        {
            enabled = false;
        }
    } 
}
