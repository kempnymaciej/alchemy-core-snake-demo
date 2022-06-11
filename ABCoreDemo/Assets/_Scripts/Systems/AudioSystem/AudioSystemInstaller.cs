using AlchemyBow.Core.IoC;
using UnityEngine;

namespace AlchemyBow.CoreDemos.Systems
{
    public sealed class AudioSystemInstaller : MonoInstaller
    {
        [SerializeField]
        private AudioSystem audioSystem;

        public override void InstallBindings(IBindOnlyContainer container)
        {
            container.Bind(audioSystem);
        }
    }
}
