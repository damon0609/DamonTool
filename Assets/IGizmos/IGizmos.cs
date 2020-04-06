using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Damon.Tool {
    public static class GizmosTool {
        public static void OnDrawSphere (Vector3 pos, float radius){
            Gizmos.DrawSphere (pos, radius);
        }
        public static void OnDrawRect () {

        }
    }

    public interface IGizmos {
        bool isShow { set; }
        Color color { set; }
        void OnDrawGizmosItem ();
    }

}