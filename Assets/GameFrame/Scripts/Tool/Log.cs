using UnityEditor;
using UnityEngine;

namespace Damon.Tool {
    public interface ILog {

    }
    public static class Log {
        public static bool on = true;
        public static void d<T> (this T log, string tag, object info, bool isShow = true) where T : ILog {
            if (on && isShow) {
                string name = log.GetType ().Name;
                Debug.Log ("<color=blue><b>[ScriptName:" + name + ",Tag:" + tag + "]</b></color>==>" + info.ToString());
            }
        }
        public static void d<T> (this T log, string tag, float info, bool isShow = true) where T : ILog {
            if (on && isShow) {
                string name = log.GetType ().Name;
                Debug.Log ("<color=blue><b>[ScriptName:" + name + ",Tag:" + tag + "]</b></color>==>" + info);
            }
        }

        public static void d<T> (this T log, string tag, bool info, bool isShow = true) where T : ILog {
            if (on && isShow) {
                string name = log.GetType ().Name;
                Debug.Log ("<color=blue><b>[ScriptName:" + name + ",Tag:" + tag + "]</b></color>==>" + info);
            }
        }

        public static void d<T> (this T log, string tag, int info, bool isShow = true) where T : ILog {
            if (on && isShow) {
                string name = log.GetType ().Name;
                Debug.Log ("<color=blue><b>[ScriptName:" + name + ",Tag:" + tag + "]</b></color>==>" + info);
            }
        }

        public static void d<T> (this T log, string tag, string info, bool isShow = true) where T : ILog {
            if (on && isShow) {
                string name = log.GetType ().Name;
                Debug.Log ("<color=blue><b>[ScriptName:" + name + ",Tag:" + tag + "]</b></color>==>" + info);
            }
        }
        public static void e<T> (this T log, string tag, string info, bool isShow = true) where T : ILog {
            if (on && isShow) {
                string name = log.GetType ().Name;
                Debug.LogError ("<color=red><b>[ScriptName:" + name + ",Tag:" + tag + "]</b></color>==>" + info);
            }
        }

        public static void w<T> (this T log, string tag, string info, bool isShow = true) where T : ILog {
            if (on && isShow) {
                string name = log.GetType ().Name;
                Debug.LogWarning ("<color=yellow><b>[ScriptName:" + name + ",Tag:" + tag + "]</b></color>==>" + info);
            }
        }
    }
}