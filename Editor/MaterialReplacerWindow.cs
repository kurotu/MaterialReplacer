using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace KRT.MaterialReplacer
{
    /// <summary>
    /// Window to apply MaterialReplacerRule to renderers.
    /// </summary>
    internal class MaterialReplacerWindow : EditorWindow
    {
        [SerializeField]
        private GameObject targetObject;
        [SerializeField]
        private MaterialReplacerRule materialReplacerRule;

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
            if (MaterialReplacer.ShouldNotifyUpdate())
            {
                using (var box = new EditorGUILayout.VerticalScope(GUI.skin.box))
                {
                    var color = GUI.contentColor;
                    GUI.contentColor = Color.red;
                    EditorGUILayout.LabelField($"Update: {MaterialReplacer.Version} -> {MaterialReplacer.latestRelease.Version}", EditorStyles.boldLabel);
                    GUI.contentColor = color;
                    using (var horizontal = new EditorGUILayout.HorizontalScope())
                    {
                        if (GUILayout.Button("Booth"))
                        {
                            Application.OpenURL(MaterialReplacer.BoothURL);
                        }
                        if (GUILayout.Button("GitHub"))
                        {
                            Application.OpenURL(MaterialReplacer.GitHubURL);
                        }
                    }
                }
                EditorGUILayout.Space();
            }

            targetObject = (GameObject)EditorGUILayout.ObjectField("Game Object", targetObject, typeof(GameObject), true);
            materialReplacerRule = (MaterialReplacerRule)EditorGUILayout.ObjectField("Material Replacer Rule", materialReplacerRule, typeof(MaterialReplacerRule), false);

            if (targetObject && materialReplacerRule)
            {
                var targetMaterials = targetObject.GetComponentsInChildren<Renderer>(true)
                    .SelectMany(r => r.sharedMaterials)
                    .Where(m => m != null)
                    .Distinct()
                    .ToArray();
                materialReplacerRule.GetPairs(replaces);
                var show = replaces.Where(pair => targetMaterials.Contains(pair.Key) && pair.Value != null).ToArray();
                if (show.Length > 0)
                {
                    EditorGUILayout.Space();
                    using (var box = new EditorGUILayout.VerticalScope(GUI.skin.box))
                    {
                        EditorGUILayout.LabelField("Materials will be replaced:", EditorStyles.boldLabel);
                        foreach (var pair in show)
                        {
                            GUILayout.Label($"- {pair.Key.name} -> {pair.Value.name}");
                        }
                    }
                }
            }

            EditorGUILayout.Space();

            using (var disbled = new EditorGUI.DisabledGroupScope(targetObject == null || materialReplacerRule == null))
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
                    if (!materialReplacerRule.ContainsKey(m))
                    {
                        return m;
                    }
                    var resolved = materialReplacerRule[m];
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
