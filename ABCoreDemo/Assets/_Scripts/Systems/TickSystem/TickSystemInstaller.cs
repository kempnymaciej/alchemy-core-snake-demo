using AlchemyBow.Core.IoC;
using UnityEngine;

namespace AlchemyBow.CoreDemos.Systems
{
    public sealed class TickSystemInstaller : MonoInstaller
    {
        [SerializeField]
        private TickSystem tickSystem;

        public override void InstallBindings(IBindOnlyContainer container)
        {
            container.Bind(tickSystem);
        }
    }
}
