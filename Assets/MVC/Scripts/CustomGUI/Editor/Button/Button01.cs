using System;
using UnityEditor;
using UnityEngine;

namespace Damon.CustomGUI {
    public class Button01 : IGUIView {
        public Action onClick;
        private GUIStyle m_defaultGUIStyle = EditorStyles.toolbarButton;
        private Rect m_Rect;
        public Rect rect {
            get { return m_Rect; }
            set { m_Rect = value; }
        }

        public GUIStyle defaultGUIStyle {
            get {
                if (m_defaultGUIStyle == null) {
                    m_defaultGUIStyle = new GUIStyle ();
                }
                return m_defaultGUIStyle;
            }
        }
        private GUIStyle m_Style;
        public GUIStyle style {
            set { m_Style = value; }
            get { return m_Style; }
        }
        private Action<Rect> m_Drawer;
        public Action<Rect> drawer {
            set { m_Drawer = value; }
            get{return m_Drawer;}
        }

        private string m_Label;
        public string label {
            set { m_Label = value; }
            get{return m_Label;}
        }
        private bool m_Active = true;
        public bool active {
            get { return m_Active; }
            set { m_Active = value; }
        }

        private bool m_Visibility = true;
        public bool visibility {
            get { return m_Visibility; }
            set { m_Visibility = value; }
        }

        private GUIStyle curStyle;
        private bool autoGUILayout = true;
        public Button01 (string name, bool guiLayout = true) {
            this.m_Label = name;
            this.autoGUILayout = guiLayout;
        }

        public Button01 (Rect rect, string name, bool guiLayout) : this (name, guiLayout) {
            this.rect = rect;
        }
        public void OnDraw () {
            if (m_Drawer != null) {
                m_Drawer (rect);
            } else {
                if (m_Style != null)
                    curStyle = m_Style;
                else
                    curStyle = m_defaultGUIStyle;
                if (autoGUILayout) {
                    if (GUILayout.Button (m_Label, curStyle)) {
                        if (onClick != null)
                            onClick ();
                    }
                } else {
                    if (GUI.Button (m_Rect, m_Label)) {
                        if (onClick != null)
                            onClick ();
                    }
                }
            }
        }
    }
}