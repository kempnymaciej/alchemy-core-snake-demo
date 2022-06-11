using AlchemyBow.Core.IoC;
using UnityEngine;

namespace AlchemyBow.CoreDemos.Avatars
{
    /// <summary>
    /// Allows you to install multiple components (<c>MonoBehaviour</c>) at once.
    /// </summary>
    public class MonoBehavioursInstaller : MonoInstaller
    {
        [SerializeField]
        private MonoBehaviour[] monoBehaviours = new MonoBehaviour[0];

        public override void InstallBindings(IBindOnlyContainer container)
        {
            foreach (var monoBehaviour in monoBehaviours)
            {
                // Bind the value from the collection to its actual type.
                container.Bind(monoBehaviour.GetType(), monoBehaviour); 
            }
        }
    }
}
