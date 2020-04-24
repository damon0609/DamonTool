using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Damon.EditorTool {
    public abstract class CustomEditorBase<T> : CustomEditorBase where T : UnityEngine.Object {

    }
    public abstract class CustomEditorBase : Editor {

    }
}