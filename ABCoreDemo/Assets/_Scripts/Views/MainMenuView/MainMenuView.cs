using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AlchemyBow.CoreDemos.Views
{
    public sealed class MainMenuView : View
    {
        [SerializeField]
        private Button playButton;
        [SerializeField]
        private Button exitButton;
        [SerializeField]
        private TMP_Text bestScore;

        public void AddPlayClickListener(UnityAction action) => playButton.onClick.AddListener(action);
        public void RemovePlayClickListener(UnityAction action) => playButton.onClick.RemoveListener(action);

        public void AddExitClickListener(UnityAction action) => exitButton.onClick.AddListener(action);
        public void RemoveExitClickListener(UnityAction action) => exitButton.onClick.RemoveListener(action);

        public void SetBestScore(string value) => bestScore.text = value;
    } 
}
