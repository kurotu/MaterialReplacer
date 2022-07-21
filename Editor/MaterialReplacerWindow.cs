using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace KRT.MaterialReplacer
{
    /// <summary>
    /// Window to apply material replace map to renderers.
    /// </summary>
    internal class MaterialReplacerWindow : EditorWindow
    {
        [SerializeField]
        private GameObject targetObject;
        [SerializeField]
        private MaterialReplaceMap materialReplaceMap;

        private List<KeyValuePair<Material, Material>> replaces = new List<KeyValuePair<Material, Material>>();

        [MenuItem("Window/Material Replacer")]
        [MenuItem("GameObject/Material Replacer", false, 30)]
        private static void ShowFromMenu()
        {
            var window = GetWindow<MaterialReplacerWindow>();
            if (Selection.activeGameObject)
            {
                window.targetObject = Selection.activeGameObject;
            }
            window.Show();
        }

        private void OnEnable()
        {
            titleContent.text = "Material Replacer";
        }

        private void OnGUI()
        {
            targetObject = (GameObject)EditorGUILayout.ObjectField("Game Object", targetObject, typeof(GameObject), true);
            materialReplaceMap = (MaterialReplaceMap)EditorGUILayout.ObjectField("Material Replace Map", materialReplaceMap, typeof(MaterialReplaceMap), false);

            if (targetObject && materialReplaceMap)
            {
                using (var box = new EditorGUILayout.VerticalScope(GUI.skin.box))
                {
                    var targetMaterials = targetObject.GetComponentsInChildren<Renderer>(true)
                        .SelectMany(r => r.sharedMaterials)
                        .Where(m => m != null)
                        .Distinct()
                        .ToArray();
                    materialReplaceMap.GetPairs(replaces);
                    foreach (var pair in replaces)
                    {
                        if (!targetMaterials.Contains(pair.Key))
                        {
                            continue;
                        }
                        if (pair.Value == null)
                        {
                            continue;
                        }
                        GUILayout.Label($"{pair.Key.name} => {pair.Value.name}");
                    }
                }
            }

            EditorGUILayout.Space();

            using (var disbled = new EditorGUI.DisabledGroupScope(targetObject == null || materialReplaceMap == null))
            {
                if (GUILayout.Button("Apply"))
                {
                    OnClickApply();
                }
            }
        }

        private void OnClickApply()
        {
            var renderers = targetObject.GetComponentsInChildren<Renderer>(true);
            for (var rendererIndex = 0; rendererIndex < renderers.Length; rendererIndex++)
            {
                var renderer = renderers[rendererIndex];
                Undo.RecordObject(renderer, "Apply Material Override");
                renderer.sharedMaterials = renderer.sharedMaterials.Select(m =>
                {
                    if (m == null)
                    {
                        return null;
                    }
                    if (!materialReplaceMap.ContainsKey(m))
                    {
                        return m;
                    }
                    var resolved = materialReplaceMap[m];
                    if (resolved)
                    {
                        return resolved;
                    }
                    return m;
                }).ToArray();
            }
        }
    }
}
