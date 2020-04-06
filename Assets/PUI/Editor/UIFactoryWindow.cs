using System.Collections;
using System.Collections.Generic;
using Damon.Tool;
using UnityEditor;
using UnityEngine;

namespace PUI {

    public class LayoutContainer {

    }
    public class UIFactoryWindow : EditorWindow, ILog {
        private Vector2 hierarchyPos;
        private Rect hierarchyRect;
        private Rect hierarchyViewRect;

        private int hierarchyCount = 10;
        public const int itemHeight = 12;

        private bool rootFoldOut = true;
        private GenericMenu rootMenu;
        private GameObject uiRoot;

        private List<PUIContainer> containers = new List<PUIContainer> ();

        private void OnEnable () {
            hierarchyViewRect = hierarchyRect = position;
            hierarchyViewRect.x = 5;
            hierarchyViewRect.y = 5;
            hierarchyRect.x = 5;
            hierarchyRect.y = 5;
            hierarchyViewRect.height = hierarchyRect.height = position.height - 10;
            hierarchyViewRect.width = hierarchyRect.width = 140;
            rootMenu = new GenericMenu ();
            uiRoot = GameObject.Find ("PUIRoot");
            containers.Clear ();

            EditorApplication.hierarchyChanged += () => {
                this.d ("damon", "----");
            };
        }
        private void OnGUI () {
            CreateHierarchy ();
        }
        private void CreateHierarchy () {
            hierarchyPos = GUI.BeginScrollView (hierarchyRect, hierarchyPos, hierarchyViewRect);
            GUI.Box (hierarchyRect, "");
            Rect labelRect = hierarchyRect;
            labelRect.x += 2;
            labelRect.y += 2;
            labelRect.height = itemHeight;
            rootFoldOut = EditorGUI.Foldout (hierarchyRect, rootFoldOut, "PUIRoot");
            GUI.EndScrollView ();

            if (!rootFoldOut) return;

            Event e = Event.current;
            if (e.isMouse && e.type == EventType.MouseDown && e.button == 1) {
                if (labelRect.Contains (e.mousePosition)) {
                    rootMenu.AddItem (new GUIContent ("添加布局容器"), false, () => {
                        RectTransform rect = GameObjectTool.NewUIGameObject("PUIContainer",uiRoot.gameObject);
                        PUIContainer container = rect.gameObject.OnAddComponent<PUIContainer>();
                        rect.sizeDelta = container.nodeInfo.size;
                        containers.Add (container);
                    });
                }
                rootMenu.ShowAsContext ();
                e.Use ();
            }
            OnDrawContainer (hierarchyRect);
        }
        private void OnDrawContainer (Rect rect) {
            rect.y += itemHeight + 5;
            rect.x += 15;
            for (int i = 0; i < containers.Count; i++) {
                PUIContainer container = containers[i];
                rect.y += i * itemHeight;
                GUI.Label (rect, container.name);
                BeginWindows ();
                Vector2 size = container.nodeInfo.size;
                Rect winRect = new Rect (position.width / 2 - size.x / 2, position.height / 2 - size.y / 2, size.x, size.y);
                winRect = GUI.Window (0, winRect, (int id) => {
                    GUI.DragWindow ();
                }, container.name);

                GUI.Label (winRect, "test" + container.name);
                EndWindows ();
            }
        }
    }
}