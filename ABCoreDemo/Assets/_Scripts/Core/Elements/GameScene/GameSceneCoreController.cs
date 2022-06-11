using AlchemyBow.Core;
using AlchemyBow.Core.IoC;
using AlchemyBow.Core.States;
using System.Collections.Generic;

namespace AlchemyBow.CoreDemos.Core.Elements
{
    // This is a core controller for the actual game scene.
    // It is similar to the `MenuSceneCoreController`,
    // but the game logic is delegated to states for easy control
    // of more complex behavior.
    [InjectionTarget]
    public sealed class GameSceneCoreController : SnakeGameCoreController
    {
        [Inject]
        private readonly MenuTrigger menuTrigger;
        [Inject]
        private readonly GameTrigger gameTrigger;
        [Inject]
        private readonly PauseSwitchingCondition pauseSwitchingCondition;

        private readonly GameState gameState = new GameState();
        private readonly GamePlayState gamePlayState = new GamePlayState();
        private readonly GamePauseState gamePauseState = new GamePauseState();

        private StateGraph stateGraph;

        // You can add some extra delay before loading.
        protected override float PreLoadingDelay => .5f;

        protected override void InstallAdditionalBindings(IBindOnlyContainer container)
        {
            // Tell the container that states need dependencies.
            container.BindInaccessible(gameState);
            container.BindInaccessible(gamePlayState);
            container.BindInaccessible(gamePauseState);

            base.InstallAdditionalBindings(container);
        }

        protected override IEnumerable<ICoreLoadable> GetLoadables()
        {
            // Note that the loadables loaded here (in the core controller)
            // are loaded every time the related scene is loaded.
            UnityEngine.Debug.LogFormat($"<color=#cc33ff>{GetType().Name}:</color> GetLoadables()");
            return null; // You can `return null` or `yield break` if there are no loadables.
        }

        protected override void OnLoadingFinished()
        {
            // Subscribe the relevant scene change triggers:
            menuTrigger.Triggered += ChangeToMenuScene;
            gameTrigger.Triggered += ChangeToGameScene;
            base.OnLoadingFinished();
            // Create a state machine and activate(enter) it.
            stateGraph = CreateStateGraph();
            stateGraph.Enter();
        }

        protected override void OnSceneChangeStarted()
        {
            // Unsubscribe the relevant scene change triggers:
            menuTrigger.Triggered -= ChangeToMenuScene;
            gameTrigger.Triggered -= ChangeToGameScene;
            // Deactivate(exit) the state machine.
            stateGraph.Exit();
            base.OnSceneChangeStarted();
        }

        private StateGraph CreateStateGraph()
        {
            var gameComposer = new StateGraphComposer(gameState); //root state
            var gamePlayComposer = new StateGraphComposer(gamePlayState);
            var gamePauseComposer = new StateGraphComposer(gamePauseState);
            gameComposer.AddNode(gamePlayComposer);
            gameComposer.AddNode(gamePauseComposer);
            gameComposer.AddLink(gamePlayComposer, gamePauseComposer, pauseSwitchingCondition);
            gameComposer.AddLink(gamePauseComposer, gamePlayComposer, pauseSwitchingCondition);

#if UNITY_EDITOR
            gameComposer.Validate();
#endif
            return StateGraph.Build(gameComposer);
        }
    }
}
