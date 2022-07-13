using AlchemyBow.Core;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace AlchemyBow.CoreDemos.Systems
{
    /// <summary>
    /// Allows you to keep the data about the best score between sessions.
    /// </summary>
    public sealed class ScoreSystem : ICoreLoadable
    {
        private const string FileName = "snake_score.abcd";
        private static readonly string SavePath = Path.Combine(Application.persistentDataPath, FileName);

        /// <summary>
        /// Returns the best score if any; otherwise, <c>null</c>.
        /// </summary>
        public Score BestScore { get; private set; }

        #region ICoreLoadable
        public void Load(OperationHandle handle)
        {
            Task.Run(async () =>
            {
                try
                {
                    string json = await File.ReadAllTextAsync(SavePath);
                    BestScore = JsonUtility.FromJson<Score>(json);
                }
                catch  { }
                handle.MarkDone();
            });
        }
        #endregion

        /// <summary>
        /// Compares the result with the current best score and keeps the best one.
        /// </summary>
        /// <param name="points">The number of points.</param>
        /// <param name="ticks">The number of ticks.</param>
        public void CheckAndUpdateBestScore(int points, int ticks)
        {
            var score = new Score(points, ticks);
            if (BestScore == null || BestScore.CompareTo(score) < 0)
            {
                BestScore = score;
                PersistBestScore();
            }
        }

        private void PersistBestScore()
        {
            if (BestScore != null)
            {
                File.WriteAllText(SavePath, JsonUtility.ToJson(BestScore)); 
            }
        }
    }
}
