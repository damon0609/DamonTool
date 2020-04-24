using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using Damon.Tool;
using UnityEditor;
using UnityEngine;
namespace Damon.EditorTool {
    public class CustomPropertyDrawerBase : PropertyDrawer {

        private string mSampleName;
        private string mSampleHeight;
        public CustomPropertyDrawerBase () {
            mSampleName = "OnGUI for" + GetType ().Name;
            mSampleHeight = "GetPropertyHeight for" + GetType ().Name;
        }
        private SerializedProperty mSerializedPro;
        public const float indentAmount = 12f; //缩进数量
        public static float spaceHeight = 2f;
        internal List<IDrawable> mDrawable;
        protected virtual void Init (SerializedProperty property) {
            if (property == mSerializedPro)
                return;
            mDrawable = new List<IDrawable> ();
            mSerializedPro = property;
        }
        public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
            using (new SampleProfile (mSampleHeight)) {
                Init (property);
                float height = 0;
                float num = 0;
                foreach (IDrawable draw in mDrawable) {
                    if (draw is PropertyContainer) {
                        num++;
                        height += ((PropertyContainer) draw).getHeight ();
                    }
                }
                height += (num - 1) * spaceHeight;
                return height;
            }
        }

        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
            using (new SampleProfile (mSampleName)) {
                Init (property);
                foreach (IDrawable draw in mDrawable) {
                    draw.Draw (ref position);
                }
            }
        }

        protected void DrawPropertyConditionally (string name, bool on) {
            SerializedProperty property;
            if (!TrySerializedProperty (name, out property)) {
                return;
            }
            GUIContent content = new GUIContent (property.displayName, property.tooltip);
            PropertyContainer propertyContainer = new PropertyContainer ();
            propertyContainer.draw += rect => {
                if (on) {
                    spaceHeight = 2f;
                    EditorGUI.PropertyField (rect, property, content);
                } else
                {
                    spaceHeight = 0;
                }
            };
            propertyContainer.getHeight = () => on?EditorGUI.GetPropertyHeight (property, GUIContent.none) : 0;
            mDrawable.Add (propertyContainer);
        }

        protected void DrawCustom (Action<Rect> action, float height) {
            mDrawable.Add (new PropertyContainer {
                draw = action,
                    getHeight = () => height
            });
        }

        protected void DrawCustom (Action<Rect> action, Func<float> height) {
            mDrawable.Add (new PropertyContainer {
                draw = action,
                    getHeight = height
            });
        }
        protected void IncreaseIndent () {
            mDrawable.Add (new IndentDrawable () {
                indent = indentAmount
            });
        }

        protected void DecreaseIndent () {
            mDrawable.Add (new IndentDrawable () {
                indent = -indentAmount
            });
        }

        protected void DrawProperty (string name, Func<string> nameFunc, bool include = true) {
            SerializedProperty property;
            if (!TrySerializedProperty (name, out property)) {
                return;
            }
            GUIContent content = new GUIContent (nameFunc (), property.tooltip);
            PropertyContainer propertyContainer = new PropertyContainer ();
            propertyContainer.draw += rect => {
                content.text = nameFunc () ?? property.displayName;
                EditorGUI.PropertyField (rect, property, content, include);
            };
            propertyContainer.getHeight = () => EditorGUI.GetPropertyHeight (property, GUIContent.none, include);
            mDrawable.Add (propertyContainer);
        }

        protected void DrawProperty (string name, bool include = true, bool disable = false) {
            SerializedProperty property;
            if (!TrySerializedProperty (name, out property)) {
                return;
            }
            GUIContent content = new GUIContent (property.displayName, property.tooltip);
            PropertyContainer propertyContainer = new PropertyContainer ();
            propertyContainer.draw += rect => {
                EditorGUI.BeginDisabledGroup (disable);
                EditorGUI.PropertyField (rect, property, content, include);
                EditorGUI.EndDisabledGroup ();
            };
            propertyContainer.getHeight = () => EditorGUI.GetPropertyHeight (property, GUIContent.none, include);
            mDrawable.Add (propertyContainer);
        }

        protected bool IsValidSerializedProperty (string name) {
            if (mSerializedPro.FindPropertyRelative (name) != null) {
                return true;
            }
            return false;
        }

        protected bool TrySerializedProperty (string name, out SerializedProperty property) {
            property = mSerializedPro.FindPropertyRelative (name);
            if (property == null)
                return false;
            return true;
        }
    }

    internal interface IDrawable {
        void Draw (ref Rect rect);
    }
    internal struct IndentDrawable : IDrawable {

        public float indent;
        public void Draw (ref Rect rect) {
            rect.x += indent;
            rect.width -= indent;
        }
    }

    internal struct PropertyContainer : IDrawable {
        public Action<Rect> draw;
        public Func<float> getHeight;
        public void Draw (ref Rect rect) {
            rect.height = getHeight ();
            draw (rect);
            rect.y += CustomPropertyDrawerBase.spaceHeight;
            rect.y += rect.height;
        }
    }
}