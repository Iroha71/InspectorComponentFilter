using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace InspectorComponentFilter {
    public class InspectorComponentFilter : EditorWindow {
        private string filter;
        private GameObject target;
        private Dictionary<string, Component> components = new Dictionary<string, Component>();
        private int instanceId;
        [MenuItem("Tools/InspectorComponentFilter")]
        private static void Init() {
            GetWindow<InspectorComponentFilter>("Search Component");
        }

        private void OnGUI() {
            if (instanceId != Selection.activeInstanceID) {
                ChangeHideFlags(components, HideFlags.None);
                instanceId = Selection.activeInstanceID;
                target = Selection.activeGameObject;
            }
            GetAttachedCompoentns(target);
            using (new EditorGUILayout.HorizontalScope()) {
                GUILayout.Label("Filter:", GUILayout.Width(45));
                GUI.SetNextControlName("filterField");
                EditorGUI.BeginChangeCheck();
                filter = GUILayout.TextField(filter, "SearchTextField", GUILayout.Width(120));
                if (EditorGUI.EndChangeCheck() && !string.IsNullOrEmpty(filter)) {
                    Dictionary<string, Component> matchedComponents = components.Where(com => com.Key.StartsWith(filter.ToLower())).ToDictionary(x => x.Key, x => x.Value);
                    if (matchedComponents.Count > 0) {
                        foreach (string key in components.Keys) {
                            if (matchedComponents.ContainsKey(key))
                                SwitchComponent(components[key], HideFlags.None);
                            else
                                SwitchComponent(components[key], HideFlags.HideInInspector);
                        }
                    }
                } else if (string.IsNullOrEmpty(filter)) {
                    foreach (string key in components.Keys) {
                        ChangeHideFlags(components, HideFlags.None);
                    }
                }
                GUI.FocusControl("filterField");
                GUI.enabled = !string.IsNullOrEmpty(filter);
                if (GUILayout.Button("Clear", "SearchCancelButton")) {
                    filter = string.Empty;
                    ChangeHideFlags(components, HideFlags.HideInInspector);
                }
                GUI.enabled = true;
            }
        }

        private void ChangeHideFlags(Dictionary<string, Component> components, HideFlags hideFlags, Dictionary<string, Component> matchedComponents=null) {
            foreach (string key in components.Keys) {
                SwitchComponent(components[key], hideFlags);
            }
        }

        private void SwitchComponent(Component component, HideFlags flags) {
            if (!component)
                return;
            if (component.hideFlags == flags)
                return;
            component.hideFlags = flags;
        }

        private void GetAttachedCompoentns(GameObject target) {
            if (!target)
                return;
            Component[] components = target.GetComponents<Component>();
            this.components.Clear();
            foreach (Component com in components) {
                this.components.Add(com.GetType().Name.ToLower(), com);
            }
        }

        private void OnDestroy() {
            ChangeHideFlags(components, HideFlags.None);
        }
    }
}
