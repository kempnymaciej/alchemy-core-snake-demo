using AlchemyBow.Core.IoC;
using UnityEngine;

namespace AlchemyBow.CoreDemos.Systems
{
    public sealed class InputSystemInstaller : MonoInstaller
    {
        [SerializeField]
        private InputSystem inputSystem;

        public override void InstallBindings(IBindOnlyContainer container)
        {
            container.Bind(inputSystem);
        }
    } 
}
