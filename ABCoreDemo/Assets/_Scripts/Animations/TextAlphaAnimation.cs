using TMPro;
using UnityEngine;

namespace AlchemyBow.CoreDemos.Animations
{
    /// <summary>
    /// A component that endlessly animates `TextMeshProUGUI.alfa` with a sine wave.
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public sealed class TextAlphaAnimation : MonoBehaviour
    {
        [SerializeField, Min(0)]
        private float speed = 1;
        [SerializeField]
        private bool unscaledTime;
        [SerializeField, Range(0, 1)]
        private float min = 0;
        [SerializeField, Range(0, 1)]
        private float max = 1;

        private TextMeshProUGUI target;

        private void Awake()
        {
            target = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            float time = unscaledTime ? Time.unscaledTime : Time.time;
            target.alpha = Mathf.Lerp(min, max, (Mathf.Sin(speed * time) + 1) / 2);
        }

        private void OnValidate()
        {
            if (min >= max)
            {
                Debug.LogError("The min value should be less then the max value.");
            }
        }
    } 
}
