using AlchemyBow.Core.IoC;

namespace AlchemyBow.CoreDemos.Systems
{
    public sealed class ScoreSystemInstaller : MonoInstaller
    {
        public override void InstallBindings(IBindOnlyContainer container)
        {
            container.Bind(new ScoreSystem());
        }
    }
}
