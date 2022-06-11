using AlchemyBow.LoadingScenes;

namespace AlchemyBow.CoreDemos
{
    /// <summary>
    /// Provides a static interface to control the loading screen.
    /// </summary>
    /// <remarks>Uses the `AlchemyBow.LoadingScenes` package. https://github.com/kempnymaciej/alchemy-loadingscenes</remarks>
    public static class LoadingSceneUtility
    {
        private const string PrefabName = "LoadingSceneContent";

        /// <summary>
        /// Ensures the loading screen is active / inactive.
        /// </summary>
        /// <param name="value">If <c>true</c> activates the loading screen; otherwise, deactivates it.</param>
        public static void EnsureLoadingSceneActive(bool value)
        {
            if (value)
            {
                LoadingScene.EnsureActive(PrefabName);
            }
            else
            {
                LoadingScene.EnsureInactive();
            }
        }
    } 
}
