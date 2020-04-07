using System;
using Damon.Tool;
using UnityEngine;

namespace PUI {
    [System.Serializable]
    public class NodeInfo {
        [SerializeField]
        public string name = "nodeName";
        [SerializeField]
        public Vector3 pos;
        [SerializeField]
        public Vector2 size = new Vector2(400,300);

    }
    public abstract class PUINode : MonoBehaviour, ILog {
      
        [SerializeField]
        public NodeInfo nodeInfo = new NodeInfo();
        protected virtual void OnDrawGizmos () {

        }
    }
}