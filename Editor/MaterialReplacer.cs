using System.IO;
using UnityEditor;

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

        private const string PackageJsonGUID = "1560de9bb4191fa478b78a2d79be403a";

        private static string AssetRoot => Path.GetDirectoryName(AssetDatabase.GUIDToAssetPath(PackageJsonGUID));

        private static void Export()
        {
            AssetDatabase.ExportPackage(AssetRoot, "MaterialReplacer.unitypackage", ExportPackageOptions.Recurse);
        }
    }
}
