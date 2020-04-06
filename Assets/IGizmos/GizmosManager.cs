using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Damon.Tool {
    public class GizmosManager : MonoBehaviour {
        public static bool isShowGizmos = true;
        private static List<IGizmos> gizmos = new List<IGizmos> ();
        public static void AddGizmos (IGizmos g) {
            if (gizmos != null)
                gizmos.Add (g);
        }

        void OnDrawGizmos () {
            foreach (var g in gizmos) {
                g.OnDrawGizmosItem ();
            }
        }
        void OnDestroy () {
            gizmos.Clear ();
        }
    }
}