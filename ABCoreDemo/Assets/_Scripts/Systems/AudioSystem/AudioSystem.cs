using UnityEngine;

namespace AlchemyBow.CoreDemos.Systems
{
    /// <summary>
    /// Allows you to play music and sound effects.
    /// </summary>
    public sealed class AudioSystem : MonoBehaviour
    {
        [SerializeField]
        private AudioSource musicSource;
        [SerializeField]
        private AudioSource effectsSource;

        private AudioClipData currentMusicClip;

        /// <summary>
        /// Playes a music clip in loop.
        /// </summary>
        /// <param name="audioClipData">The audio clip data.</param>
        /// <param name="forceReset">If <c>true</c>, restarts the music, even if the clip is the same.</param>
        public void SetMusic(AudioClipData audioClipData, bool forceReset)
        {
            if (forceReset || currentMusicClip != audioClipData)
            {
                currentMusicClip = audioClipData;
                musicSource.Stop();
                
                if (audioClipData != null && audioClipData.AudioClip != null)
                {
                    musicSource.clip = audioClipData.AudioClip;
                    musicSource.volume = audioClipData.Volume;
                    musicSource.Play();
                }
                else
                {
                    musicSource.clip = null;
                    musicSource.volume = 1;
                    Debug.LogWarning("Failed to set the music.");
                }
            }
        }

        /// <summary>
        /// Plays a SFX clip (once).
        /// </summary>
        /// <param name="audioClipData">The SFX clip data.</param>
        public void PlayEffect(AudioClipData audioClipData)
        {
            if (audioClipData != null && audioClipData.AudioClip != null)
            {
                effectsSource.PlayOneShot(audioClipData.AudioClip, audioClipData.Volume);
            }
            else
            {
                Debug.LogWarning("Failed to play the effect.");
            }
        }
    } 
}
