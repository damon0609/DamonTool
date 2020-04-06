using System.Collections.Generic;
using UnityEngine;
namespace Damon.Tool {

    public enum ComponentType {
        RectTransform,
        Transform,
    }
    public static class GameObjectTool {

        public static RectTransform NewUIGameObject(string name,GameObject parent = null)
        {
            GameObject go = new GameObject(name);
            RectTransform rect = go.AddComponent<RectTransform>();
            if(parent!=null)
            {
                go.transform.parent = parent.transform;
            }
            rect.localScale = Vector3.one;
            rect.anchoredPosition3D = Vector3.zero;
            return rect;
        }

        public static GameObject Disable<T> (this GameObject go) where T : MonoBehaviour {
            go.GetComponent<T> ().enabled = false;
            return go;
        }

        public static GameObject Enable<T> (this GameObject go) where T : MonoBehaviour {
            go.GetComponent<T> ().enabled = true;
            return go;
        }

        public static T OnAddComponent<T> (this GameObject prefab) where T : Component {
            return prefab.AddComponent<T> ();
        }

        public static T OnGetComponent<T> (this GameObject prefab) where T : Component {
            return prefab.GetComponent<T> ();
        }

        public static void SetPositionHorizontal (List<GameObject> list, Vector3 pos, Vector3 intervalPos, ComponentType type) {
            Vector3 temp = Vector3.zero;
            for (int i = 0; i < list.Count; i++) {
                temp = new Vector3 (pos.x + intervalPos.x * i, pos.y, pos.z);
                SetPosition (list[i], temp, type);
            }
        }
        public static List<GameObject> GenerateGos (GameObject prefab, int count, GameObject parent) {
            List<GameObject> list = new List<GameObject> ();
            for (int i = 0; i < count; i++) {
                GameObject clone = GameObject.Instantiate<GameObject> (prefab);
                clone.transform.SetParent (parent.transform);
                list.Add (clone);
            }
            return list;
        }

        public static GameObject Instantiated (this GameObject prefab, string name) {
            GameObject go = Instantiated (prefab);
            go.name = name;
            return go;
        }
        public static GameObject Instantiated (this GameObject prefab) {
            return GameObject.Instantiate<GameObject> (prefab);
        }
        public static GameObject Instantiated (this GameObject prefab, GameObject parent) {
            GameObject go = GameObject.Instantiate<GameObject> (prefab);
            go.transform.SetParent (parent.transform);
            return go;
        }

        public static GameObject SetParent (this GameObject prefab, GameObject parent) {
            prefab.transform.SetParent (parent.transform);
            return prefab;
        }
        public static GameObject SetPosition (this GameObject prefab, Vector3 pos, ComponentType type = ComponentType.Transform) {
            if (type == ComponentType.Transform) {
                prefab.transform.position = pos;
            } else if (type == ComponentType.RectTransform) {
                RectTransform rect = prefab.GetComponent<RectTransform> ();
                rect.anchoredPosition = pos;
            }
            return prefab;
        }
        public static GameObject SetActive (this GameObject prefab, bool isActive) {
            prefab.SetActive (isActive);
            return prefab;
        }
        public static GameObject SetLayer (this GameObject prefab, int layer) {
            prefab.layer = layer;
            return prefab;
        }

        public static void DestroySelf (this GameObject prefab) {
            GameObject.Destroy (prefab);
        }
    }
}