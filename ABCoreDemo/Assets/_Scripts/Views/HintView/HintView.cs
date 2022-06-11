using TMPro;
using UnityEngine;

namespace AlchemyBow.CoreDemos.Views
{
    public sealed class HintView : View
    {
        [SerializeField]
        private TMP_Text hint;

        public void SetHind(string value) => hint.text = value;
    } 
}
