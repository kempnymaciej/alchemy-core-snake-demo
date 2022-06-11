using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AlchemyBow.CoreDemos.Views
{
    public sealed class GameResultView : View
    {
        [SerializeField]
        private TMP_Text message;
        [SerializeField]
        private Button resetButton;
        [SerializeField]
        private Button menuButton;

        public void SetMessage(string value) => message.text = value;

        public void AddResetClickListener(UnityAction action) 
            => resetButton.onClick.AddListener(action);
        public void RemoveResetClickListener(UnityAction action) 
            => resetButton.onClick.RemoveListener(action);

        public void AddMenuClickListener(UnityAction action)
            => menuButton.onClick.AddListener(action);
        public void RemoveMenuClickListener(UnityAction action) 
            => menuButton.onClick.RemoveListener(action);
    } 
}
