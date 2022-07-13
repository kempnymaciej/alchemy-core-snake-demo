using AlchemyBow.Core;
using System;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

namespace AlchemyBow.CoreDemos.Systems
{
    /// <summary>
    /// Allows you to connect to the AlchemyBow.Core repositiory and check the latest version.
    /// </summary>
    public sealed class CoreVersionSystem : MonoBehaviour, ICoreLoadable
    {
        /// <summary>
        /// The URL address to fetch the tags.
        /// </summary>
        public const string TagsUrl = "https://api.github.com/repos/kempnymaciej/alchemy-core/tags";

        /// <summary>
        /// Returns the latest version of AlchemyBow.Core if successfully connected to the repository; otherwise, <c>null</c>.
        /// </summary>
        public Version Version { get; private set; }

        public void Load(OperationHandle handle)
        {
            StartCoroutine(CreateLoadCoroutine(handle));
        }

        private IEnumerator CreateLoadCoroutine(OperationHandle handle)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(TagsUrl))
            {
                webRequest.timeout = 2;
                yield return webRequest.SendWebRequest();
                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    var tags = TagListUtility.FromJson(webRequest.downloadHandler.text);
                    Version = tags.Where(t => t.IsVersion()).Select(t => t.ConvertToVersion()).Max();
                }
            }
            handle.MarkDone();
        }

        /// <summary>
        /// Utility class to get the tag list from the result of the HTTP request.
        /// </summary>
        /// <remarks>It is needed because <c>UnityEngine.JsonUtility</c> cannot handle the json format it self.</remarks>
        [Serializable]
        private class TagListUtility
        {
            public Tag[] list;

            /// <summary>
            /// Converts a json file to an array of tags.
            /// </summary>
            /// <param name="json">The json file.</param>
            /// <returns>An array of tags.</returns>
            public static Tag[] FromJson(string json)
            {
                return JsonUtility.FromJson<TagListUtility>($"{{\n\"list\":{json}}}").list;
            }
        }

        /// <summary>
        /// Represents a tag.
        /// </summary>
        [Serializable]
        private class Tag
        {
            private readonly static Regex VersionTagNameRegex = new Regex(@"^v\d*\.\d*\.\d*$");

            /// <summary>
            /// The tag name.
            /// </summary>
            public string name;

            /// <summary>
            /// Is is a version tag?
            /// </summary>
            /// <returns><c>true</c> if it is a version tag; otherwise, <c>false</c>.</returns>
            public bool IsVersion()
            {
                return VersionTagNameRegex.IsMatch(name);
            }

            /// <summary>
            /// Returns a version that represents the tag.
            /// </summary>
            /// <returns>A version that represents the tag</returns>
            public Version ConvertToVersion()
            {
                if (!IsVersion())
                {
                    throw new Exception($"{name} is not a version");
                }
                var components = name.Split(".");
                components[0] = components[0][1..];
                return new Version(int.Parse(components[0]), int.Parse(components[1]), int.Parse(components[2]));
            }
        }
    }
}
