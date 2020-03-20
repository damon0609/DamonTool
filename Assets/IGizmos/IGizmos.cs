using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Damon.Tool
{
    public class GizmosTool
    {
        public static void DrawSphere(Vector3 pos, float radius)
        {
            Gizmos.DrawSphere(pos, radius);
        }
    }

    public interface IGizmos
    {
        bool isShow { set; }
        Color color { set; }
        void OnDrawGizmosItem();
    }


}


