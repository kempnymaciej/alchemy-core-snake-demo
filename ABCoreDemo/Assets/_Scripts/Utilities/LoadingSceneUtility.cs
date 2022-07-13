using AlchemyBow.LoadingScenes;
using TMPro;

namespace AlchemyBow.CoreDemos
{
    /// <summary>
    /// Provides a static interface to control the loading screen.
    /// </summary>
    /// <remarks>Uses the `AlchemyBow.LoadingScenes` package. https://github.com/kempnymaciej/alchemy-loadingscenes</remarks>
    public static class LoadingSceneUtility
    {
        private const string PrefabName = "LoadingSceneContent";
        private static TMP_Text loadingSceneText;

        /// <summary>
        /// Ensures the loading screen is active / inactive.
        /// </summary>
        /// <param name="value">If <c>true</c> activates the loading screen; otherwise, deactivates it.</param>
        public static void EnsureLoadingSceneActive(bool value)
        {
            if (value)
            {
                LoadingScene.EnsureActive(PrefabName);
                loadingSceneText = LoadingScene.ActiveSceneContent.GetComponentInChildren<TMP_Text>();
            }
            else
            {
                loadingSceneText = null;
                LoadingScene.EnsureInactive();
            }
        }

        public static void SetLoadingText(string value)
        {
            loadingSceneText.text = value;
        }
    } 
}
