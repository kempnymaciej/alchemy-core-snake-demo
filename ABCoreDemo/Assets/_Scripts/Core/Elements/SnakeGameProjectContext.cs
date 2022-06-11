using AlchemyBow.Core;
using AlchemyBow.Core.IoC;
using AlchemyBow.CoreDemos.Systems;
using System.Collections.Generic;

namespace AlchemyBow.CoreDemos.Core.Elements
{
    // This is a core project context for the snake game.
    [InjectionTarget]
    public sealed class SnakeGameProjectContext : CoreProjectContext
    {
        [Inject]
        private readonly ScoreSystem scoreSystem;

        protected override IEnumerable<ICoreLoadable> GetLoadables()
        {
            // Note that the loadables loaded here (in the project context) are only loaded once,
            // regardless of subsequent scene changes.
            UnityEngine.Debug.LogFormat($"<color=#0099cc>{GetType().Name}:</color> GetLoadables()");
            yield return scoreSystem;
        }
    }
}
