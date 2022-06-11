using AlchemyBow.Core.IoC;

namespace AlchemyBow.CoreDemos.Core
{
    public sealed class PauseSwitchingConditionInstaller : MonoInstaller
    {
        public override void InstallBindings(IBindOnlyContainer container)
        {
            container.Bind(new PauseSwitchingCondition());
        }
    }
}
