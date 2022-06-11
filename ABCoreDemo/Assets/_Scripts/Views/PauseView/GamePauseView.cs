using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AlchemyBow.CoreDemos.Views
{
    public sealed class GamePauseView : View
    {
        [SerializeField]
        private Button resumeButton;
        [SerializeField]
        private Button menuButton;

        public void AddResumeClickListener(UnityAction action) => resumeButton.onClick.AddListener(action);
        public void RemoveResumeClickListener(UnityAction action) => resumeButton.onClick.RemoveListener(action);

        public void AddMenuClickListener(UnityAction action) => menuButton.onClick.AddListener(action);
        public void RemoveMenuClickListener(UnityAction action) => menuButton.onClick.RemoveListener(action);
    } 
}
