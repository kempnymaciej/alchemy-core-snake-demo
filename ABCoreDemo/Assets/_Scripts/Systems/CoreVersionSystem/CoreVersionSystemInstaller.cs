using AlchemyBow.Core.IoC;
using UnityEngine;

namespace AlchemyBow.CoreDemos.Systems
{
    public class CoreVersionSystemInstaller : MonoInstaller
    {
        [SerializeField]
        private CoreVersionSystem coreVersionSystem;

        public override void InstallBindings(IBindOnlyContainer container)
        {
            container.Bind(coreVersionSystem);
        }
    } 
}
