using AlchemyBow.Core.IoC;
using UnityEngine;

namespace AlchemyBow.CoreDemos.Core.Elements
{
    public sealed class SnakeAutioClipSetInstaller : MonoInstaller
    {
        [SerializeField]
        private SnakeAudioClipSet snakeAudioClipSet;

        public override void InstallBindings(IBindOnlyContainer container)
        {
            container.Bind(snakeAudioClipSet);
        }
    } 
}
