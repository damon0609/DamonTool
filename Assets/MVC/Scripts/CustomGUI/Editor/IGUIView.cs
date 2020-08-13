using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IGUIView {
    Rect rect { get; set; }
    void OnDraw ();
    GUIStyle defaultGUIStyle { get; }
    GUIStyle style { get; set; }
    Action<Rect> drawer { set; }

    bool active{get;set;}
    bool visibility {get;set;}

}