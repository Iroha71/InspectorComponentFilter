using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace InspectorComponentFilter {
    /// <summary>
    /// オブジェクトの特定のコンポーネントをウィンドウに表示する機能を実装する
    /// </summary>
    public class InspectorComponentFilter : EditorWindow {
        /// <summary>
        /// 検索するコンポーネント名
        /// </summary>
        private string searchComponentName;
        private GameObject obj;
        private Dictionary<string, Editor> drawEditors = new Dictionary<string, Editor>();
        /// <summary>
        /// オブジェクトにアタッチされているすべてのコンポーネント
        /// </summary>
        private Dictionary<string, Component> attachedComponents = new Dictionary<string, Component>();
        /// <summary>
        /// コンポーネント一覧の現在のスクロール位置
        /// </summary>
        private Vector2 componentViewScrollPosition = Vector2.zero;
        [MenuItem("Tools/InspectorComponentFilter")]
        private static void Open() {
            GetWindow<InspectorComponentFilter>();
        }

        /// <summary>
        /// コンポーネント検索欄を描画する
        /// </summary>
        private void DrawSearchBox() {
            using (new EditorGUILayout.HorizontalScope()) {
                GUILayout.Label("Filter:", GUILayout.Width(45));
                GUI.SetNextControlName("filterField");
                searchComponentName = GUILayout.TextField(searchComponentName, "SearchTextField", GUILayout.Width(120));
                if (GUILayout.Button("Clear", "SearchCancelButton")) {
                    searchComponentName = string.Empty;
                }
            }
        }

        private void OnGUI() {
            EditorGUI.BeginChangeCheck();
            obj = EditorGUILayout.ObjectField("Obj", obj, typeof(GameObject), true) as GameObject;
            DrawSearchBox();
            
            if (EditorGUI.EndChangeCheck() && obj != null && searchComponentName != string.Empty) {
                attachedComponents = GetAttachedComponents(obj);
                drawEditors = FindTargetComponent(searchComponentName);
            } 
            DrawMatchedComponentList(drawEditors);
        }

        /// <summary>
        /// オブジェクトに取り付けられている全てのコンポーネントを取得する
        /// </summary>
        /// <param name="obj">コンポーネントを取得するオブジェクト</param>
        /// <returns>コンポーネント名を小文字に変換した辞書</returns>
        private Dictionary<string, Component> GetAttachedComponents(GameObject obj) {
            Component[] components = obj.GetComponents<Component>();
            Dictionary<string, Component> attachedComponents = new Dictionary<string, Component>();
            foreach (Component attachedComponent in components) {
                attachedComponents.Add(attachedComponent.GetType().Name.ToLower(), attachedComponent);
            }

            return attachedComponents;
        }

        /// <summary>
        /// 検索条件に合致したコンポーネントを一覧表示する
        /// </summary>
        /// <param name="matchedComponentEditors">検索条件に一致したコンポーネントのEditor情報</param>
        private void DrawMatchedComponentList(Dictionary<string, Editor> matchedComponentEditors) {
            if (matchedComponentEditors == null)
                return;
            
            componentViewScrollPosition = EditorGUILayout.BeginScrollView(componentViewScrollPosition);
            foreach (string componentName in matchedComponentEditors.Keys) {
                GUILayout.Space(10);
                GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
                EditorGUILayout.LabelField(componentName);
                matchedComponentEditors[componentName].OnInspectorGUI();
            }
            EditorGUILayout.EndScrollView();
        }

        /// <summary>
        /// 特定の名称のコンポーネントを検索し、Editor情報を返す
        /// </summary>
        /// <param name="serachComponentName">検索したいコンポーネント名</param>
        /// <returns>Inspectorに描画するEditor情報</returns>
        private Dictionary<string, Editor> FindTargetComponent(string searchComponentName) {
            Dictionary<string, Component> matchedComponents;
            if (!string.IsNullOrEmpty(searchComponentName)) {
                 matchedComponents = attachedComponents
                .Where(attachedComponent => attachedComponent.Key.StartsWith(searchComponentName.ToLower()))
                .ToDictionary(x => x.Key, x => x.Value);
            } else {
                matchedComponents = attachedComponents;   
            }

            if (matchedComponents.Count <= 0)
                return null;

            Dictionary<string, Editor> matchedComponentEditors = new Dictionary<string, Editor>();
            foreach (string key in matchedComponents.Keys) {
                matchedComponentEditors.Add(matchedComponents[key].GetType().Name, Editor.CreateEditor(matchedComponents[key]));
            }

            return matchedComponentEditors;
        }
    }
}
