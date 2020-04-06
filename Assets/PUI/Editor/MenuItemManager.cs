using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PUI {
    public class MenuItemManager {

        [MenuItem ("PUI/Create PUIRoot")]
        public static void CreatePUIRoot () {
            GameObject PUIRootGo = new GameObject ("PUIRoot");
            PUIRoot puiRoot = PUIRootGo.AddComponent<PUIRoot> ();
            Canvas canvas = PUIRootGo.AddComponent<Canvas> ();

            if (canvas.renderMode == RenderMode.WorldSpace) {
                canvas.worldCamera = GameObject.FindObjectOfType<Camera> ();
            }

            RectTransform puiRectTransform = PUIRootGo.GetComponent<RectTransform> ();
            puiRectTransform.sizeDelta = new Vector2 (2880, 1600);
            puiRectTransform.localScale = Vector3.one / 1000;
            puiRectTransform.anchoredPosition3D = new Vector3 (0, 1.67f, 1);

            PUIRootGo.AddComponent<CanvasScaler> ();
            PUIRootGo.AddComponent<GraphicRaycaster> ();

            GameObject eventSystemGo = new GameObject ("EventSystem");
            eventSystemGo.AddComponent<EventSystem> ();
            eventSystemGo.AddComponent<StandaloneInputModule> ();
        }

        [MenuItem ("PUI/UI Factory")]
        public static void CreateUIFactory () {
            UIFactoryWindow win = EditorWindow.GetWindow<UIFactoryWindow> ();
            win.titleContent = new GUIContent ("UI Fractory");
            win.Show ();
        }
    }
}