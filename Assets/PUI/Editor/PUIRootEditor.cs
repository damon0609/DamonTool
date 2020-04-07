using System.Collections;
using System.Collections.Generic;
using Damon.Tool;
using UnityEditor;
using UnityEngine;
namespace PUI {

    [CustomEditor (typeof (PUIRoot))]
    public class PUIRootEditor : Editor {

        PUIRoot t;
        private RectTransform mRectTransform;
        private void OnEnable () {
            t = target as PUIRoot;
            mRectTransform = t.gameObject.GetComponent<RectTransform> ();
            t.distance = mRectTransform.anchoredPosition3D.z;
            t.height = mRectTransform.anchoredPosition3D.y;
        }
        public override void OnInspectorGUI () {

            EditorGUILayout.Space ();

            EditorGUILayout.BeginVertical();
            t.distance = EditorGUILayout.Slider ("Distance", t.distance,1,7);
            t.height = EditorGUILayout.FloatField ("Height", t.height);

            if (mRectTransform.anchoredPosition3D.z != t.distance) {
                mRectTransform.anchoredPosition3D = new Vector3 (0, t.height, t.distance);
                mRectTransform.localScale = (Vector3.one / 1000) * t.distance;
            }

            if (mRectTransform.anchoredPosition3D.y != t.height) {
                mRectTransform.anchoredPosition3D = new Vector3 (0, t.height, t.height);
            }

            

            EditorGUILayout.EndVertical();
        }
    }
}