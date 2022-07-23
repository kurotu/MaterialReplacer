using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace KRT.MaterialReplacer
{
    /// <summary>
    /// Main class of MaterialReplacer.
    /// </summary>
    public static class MaterialReplacer
    {
        /// <summary>
        /// Version of MaterialReplacer.
        /// </summary>
        public static readonly string Version = "0.0.0";

        /// <summary>
        /// URL for booth.pm.
        /// </summary>
        internal const string BoothURL = "https://kurotu.booth.pm/items/4023240";

        /// <summary>
        /// URL for GitHub.
        /// </summary>
        internal const string GitHubURL = "https://github.com/" + Repository;

        /// <summary>
        /// Internal logger.
        /// </summary>
        internal static readonly ILogger Logger = new Logger(new LogHandler());

        /// <summary>
        /// Latest release info.
        /// </summary>
        internal static SemVer latestVersion = null;

        private const string PackageJsonGUID = "1560de9bb4191fa478b78a2d79be403a";

        private const string Repository = "kurotu/MaterialReplacer";

        private const string Tag = "MaterialReplacer";

        private static readonly HttpClient Client = new HttpClient();

        static MaterialReplacer()
        {
            var isRelease = true;
            if (isRelease)
            {
                Logger.filterLogType = LogType.Warning;
            }

            Client.Timeout = TimeSpan.FromSeconds(10);
            Client.DefaultRequestHeaders.Add("User-Agent", $"MaterialReplacer-{Version}");

            Task.Run(async () =>
            {
                try
                {
                    var release = await GetLatestRelease();
                    if (release != null)
                    {
                        latestVersion = release.Version;
                    }
                }
                catch (Exception e)
                {
                    Logger.LogException(e);
                    latestVersion = null;
                }
            });
        }

        private static string AssetRoot => Path.GetDirectoryName(AssetDatabase.GUIDToAssetPath(PackageJsonGUID));

        private static void Export()
        {
            AssetDatabase.ExportPackage(AssetRoot, "MaterialReplacer.unitypackage", ExportPackageOptions.Recurse);
        }

        private static async Task<GitHubRelease> GetLatestRelease()
        {
            var url = $"https://api.github.com/repos/{Repository}/releases/latest";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            var response = await Client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                Logger.LogError(Tag, $"Failed {request.Method} {request.RequestUri}: {(int)response.StatusCode} {response.ReasonPhrase}");
                return null;
            }
            var body = await response.Content.ReadAsStringAsync();
            var release = JsonUtility.FromJson<GitHubRelease>(body);
            return release;
        }

        private class LogHandler : ILogHandler
        {
            public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
            {
                Debug.unityLogger.logHandler.LogFormat(logType, context, format, args);
            }

            public void LogException(Exception exception, UnityEngine.Object context)
            {
                Debug.unityLogger.LogException(exception, context);
            }
        }
    }
}
