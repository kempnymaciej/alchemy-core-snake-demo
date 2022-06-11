using AlchemyBow.Core.IoC;
using UnityEngine;

namespace AlchemyBow.CoreDemos.Views
{
    /// <summary>
    /// Allows you to install multiple views at once.
    /// </summary>
    public sealed class ViewsInstaller : MonoInstaller
    {
        [SerializeField]
        private View[] views;

        public override void InstallBindings(IBindOnlyContainer container)
        {
            foreach (var view in views)
            {
                container.Bind(view.GetType(), view);
            }
        }
    }
}
