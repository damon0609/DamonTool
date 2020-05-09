using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Damon.Tool {
    public enum ShapeType
    {
        Line,
        Circle,
        Rectangle,
    }
    public abstract class BaseShape
    {
        protected ShapeType mShape;
        public void OnDrawShape()
        {

        }
    }
    public class Line : BaseShape
    {
        public Vector3 start;
        public Vector3 end;
    }

    public class GizmosManager : MonoBehaviour {

        public static bool isShowGizmos = true;
        private static List<ICustomGizmos> gizmos = new List<ICustomGizmos> ();
        private static List<BaseShape> mShapes = new List<BaseShape>();

        public static void AddShape(BaseShape shape)
        {
            mShapes.Add(shape);
        }
        public static void AddGizmos (ICustomGizmos g) {
            if (gizmos != null)
                gizmos.Add (g);
        }
        void OnDrawGizmos () {
            foreach (ICustomGizmos g in gizmos)
            {
                g.OnInit();
                g.OnDrawCustomGizmos();
            }
            foreach (BaseShape s in mShapes)
            {
                s.OnDrawShape();
            }
        }
        void OnDestroy () {
            gizmos.Clear ();
            mShapes.Clear();
        }
    }
}