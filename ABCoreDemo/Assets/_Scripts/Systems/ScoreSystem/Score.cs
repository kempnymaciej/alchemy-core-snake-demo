using System;
using UnityEngine;

namespace AlchemyBow.CoreDemos.Systems
{
    /// <summary>
    /// Represents a score.
    /// </summary>
    [Serializable]
    public sealed class Score : IComparable<Score>
    {
        [SerializeField]
        private int points;
        [SerializeField]
        private int ticks;

        public Score(int points, int ticks)
        {
            this.points = points;
            this.ticks = ticks;
        }

        /// <summary>
        /// The number of points.
        /// </summary>
        public int Points => points;

        /// <summary>
        /// The number of ticks.
        /// </summary>
        public int Ticks => ticks;

        public int CompareTo(Score other)
        {
            // The more points, the better the score.
            int result = points.CompareTo(other.points);
            if (result == 0)
            {
                // The less ticks, the better the score.
                result = -ticks.CompareTo(other.ticks);
            }
            return result;
        }
    } 
}
