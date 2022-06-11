using UnityEngine;

namespace AlchemyBow.CoreDemos
{
    /// <summary>
    /// A container for audio data.
    /// </summary>
    [CreateAssetMenu(fileName = "_ACD", menuName = "SnakeGame/Audio/AudioClip")]
    public sealed class AudioClipData : ScriptableObject
    {
        [SerializeField]
        private AudioClip audioClip;
        [SerializeField, Min(0)]
        private float volume = 1;

        public AudioClip AudioClip => audioClip;
        public float Volume => volume;
    } 
}
