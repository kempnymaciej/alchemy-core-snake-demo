using AlchemyBow.Core.IoC;
using UnityEngine;

namespace AlchemyBow.CoreDemos.Systems
{
    public sealed class SnakeSystemInstaller : MonoInstaller
    {
        [SerializeField]
        private Vector2Int mapSize;

        public override void InstallBindings(IBindOnlyContainer container)
        {
            container.Bind(new SnakeSystem(mapSize, mapSize / 2));
        }
    }
}
