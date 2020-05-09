using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Damon.Tool {
    public class GizmosTool {
        public static void OnDrawSphere (Vector3 pos, float radius) {
            Gizmos.DrawSphere (pos, radius);
        }
        public static void OnDrawRect () {

        }
        public static void AddQuad (List<Vector3> verts, List<int> indexes, Vector3 xAxis, Vector3 yAxis, Vector3 zAxis) {

            indexes.Add (verts.Count + 0);
            indexes.Add (verts.Count + 1);
            indexes.Add (verts.Count + 2);
            indexes.Add (verts.Count + 3);

            Vector3 r1 = 0.5f * (-xAxis + yAxis + zAxis);
            Vector3 r2 = 0.5f * (xAxis + yAxis + zAxis);
            Vector3 r3 = 0.5f * (xAxis - yAxis + zAxis);
            Vector3 r4 = 0.5f * (-xAxis - yAxis + zAxis);

            verts.Add (r1);
            verts.Add (r2);
            verts.Add (r3);
            verts.Add (r4);
        }
    }
}