using AlchemyBow.Core.IoC;
using UnityEngine;

namespace AlchemyBow.CoreDemos.Controllers
{
    public sealed class SnakeObserverCameraControllerInstaller : MonoInstaller
    {
        [SerializeField]
        private SnakeObserverCameraController cameraController;

        public override void InstallBindings(IBindOnlyContainer container)
        {
            container.Bind(cameraController);
        }
    }
}
