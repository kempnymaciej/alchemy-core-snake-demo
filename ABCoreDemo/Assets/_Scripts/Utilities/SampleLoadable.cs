using AlchemyBow.Core;
using System.Threading.Tasks;
using UnityEngine;

namespace AlchemyBow.CoreDemos
{
    /// <summary>
    /// A sample implementation of <c>ICoreLoadable</c>. The only reason to use it is to demonstrate how <c>ICoreLoadable</c> works.
    /// </summary>
    public sealed class SampleLoadable : ICoreLoadable
    {
        private readonly string name;
        private readonly int milisecondsDelay;

        public SampleLoadable(string name, int milisecondsDelay)
        {
            this.name = name;
            this.milisecondsDelay = milisecondsDelay;
        }

        public void Load(OperationHandle handle)
        {
            Debug.LogFormat($"{GetType().Name} ({name}): <color=#ccffcc>Loading started.</color>");
            Task.Run(async () =>
            {
                await Task.Delay(milisecondsDelay);
                handle.MarkDone();
                Debug.LogFormat($"{GetType().Name} ({name}): <color=#ffffff>Loading finished.</color>");
            });
        }
    }
}
