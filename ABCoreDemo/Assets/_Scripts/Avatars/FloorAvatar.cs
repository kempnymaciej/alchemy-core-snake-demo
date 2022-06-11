using System.Collections.Generic;
using UnityEngine;

namespace AlchemyBow.CoreDemos.Avatars
{
    /// <summary>
    /// Visualizes a floor consisting of segments.
    /// </summary>
    public sealed class FloorAvatar : MonoBehaviour 
    {
        [SerializeField]
        private float segmentHeight = -.5f;
        [SerializeField]
        private Transform segmentPrefab;

        private readonly List<Transform> segments = new List<Transform>();

        /// <summary>
        /// Visualizes a floor with a specified origin and size.
        /// </summary>
        /// <param name="origin">The origin point.</param>
        /// <param name="size">How far to draw from the origin? (Only positive values are supported.)</param>
        /// <remarks>Converts (x,y) into (x, segmentHeight, y).</remarks>
        public void SetFloor(Vector2Int origin, Vector2Int size)
        {
            ClearFloor();
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    segments.Add(Instantiate(
                        segmentPrefab, 
                        new Vector3(origin.x + x, segmentHeight, origin.y + y), 
                        Quaternion.identity, 
                        transform
                    ));
                }
            }
        }

        /// <summary>
        /// Clears any set visualization.
        /// </summary>
        public void ClearFloor()
        {
            foreach (var segment in segments)
            {
                Destroy(segment.gameObject);
            }
            segments.Clear();
        }
    } 
}
