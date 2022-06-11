using UnityEngine;

namespace AlchemyBow.CoreDemos.Core.Elements
{
    /// <summary>
    /// Represents a set of audio clips for the snake game.
    /// </summary>
    [System.Serializable]
    public sealed class SnakeAudioClipSet
    {
        [SerializeField]
        private AudioClipData move;
        [SerializeField]
        private AudioClipData eat;
        [SerializeField]
        private AudioClipData failure;

        public AudioClipData Move => move;
        public AudioClipData Eat => eat;
        public AudioClipData Failure => failure;
    } 
}
