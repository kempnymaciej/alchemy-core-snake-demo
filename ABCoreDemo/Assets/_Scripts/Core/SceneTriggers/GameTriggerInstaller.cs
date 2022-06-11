using AlchemyBow.Core.IoC;

namespace AlchemyBow.CoreDemos.Core
{
    public sealed class GameTriggerInstaller : MonoInstaller
    {
        public override void InstallBindings(IBindOnlyContainer container)
        {
            container.Bind(new GameTrigger());
        }
    }
}
