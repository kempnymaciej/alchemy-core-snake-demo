using System.Collections.Generic;
using UnityEngine;

namespace AlchemyBow.CoreDemos.Avatars
{
    /// <summary>
    /// Visualizes a snake consisting of segments.
    /// </summary>
    public sealed class SnakeAvatar : MonoBehaviour
    {
        [SerializeField]
        private Transform segmentPrefab;
        [SerializeField]
        private float headScale = 1;
        [SerializeField]
        private float bodyScale = 1;
        [SerializeField]
        private float segmentHeight = 0;

        private readonly List<Transform> segments = new List<Transform>();
        private int numberOfActiveSegments = 0;

        /// <summary>
        /// Visualizes a snake represented by a list of position (with a head at [0]).
        /// </summary>
        /// <param name="segmentPositions">Ordered positions of the snake segments, with the head position at [0].</param>
        /// <remarks>Converts (x,y) into (x, segmentHeight, y).</remarks>
        public void SetSegments(IReadOnlyList<Vector2Int> segmentPositions)
        {
            int targetNumberOfSegments = segmentPositions.Count;
            while (segments.Count < targetNumberOfSegments)
            {
                segments.Add(Instantiate(segmentPrefab, transform));
            }
            while (numberOfActiveSegments < targetNumberOfSegments)
            {
                segments[numberOfActiveSegments].gameObject.SetActive(true);
                numberOfActiveSegments++;
            }
            while (numberOfActiveSegments > targetNumberOfSegments)
            {
                numberOfActiveSegments--;
                segments[numberOfActiveSegments].gameObject.SetActive(false);
            }

            if (segments.Count > 0)
            {
                segments[0].position = new Vector3(segmentPositions[0].x, segmentHeight, segmentPositions[0].y);
                segments[0].localScale = headScale * Vector3.one;

                Vector3 bodyScale = this.bodyScale * Vector3.one;
                for (int i = 1; i < targetNumberOfSegments; i++)
                {
                    segments[i].position = new Vector3(segmentPositions[i].x, segmentHeight, segmentPositions[i].y);
                    segments[i].localScale = bodyScale;
                }
            }
        }

        /// <summary>
        /// Clears any set visualization.
        /// </summary>
        public void ClearSegments()
        {
            for (int i = 0; i < numberOfActiveSegments; i++)
            {
                segments[i].gameObject.SetActive(false);
            }
            numberOfActiveSegments = 0;
        }
    } 
}
