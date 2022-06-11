using AlchemyBow.Core.IoC;
using UnityEngine;

namespace AlchemyBow.CoreDemos
{
    /// <summary>
    /// Allows you to group your installers with composite installers.
    /// </summary>
    public class CompositeMonoInstaller : MonoInstaller
    {
        [SerializeField]
        private MonoInstaller[] subInstallers;

        public override void InstallBindings(IBindOnlyContainer container)
        {
            foreach (var subInstaller in subInstallers)
            {
                subInstaller.InstallBindings(container);
            }
        }
    } 
}
