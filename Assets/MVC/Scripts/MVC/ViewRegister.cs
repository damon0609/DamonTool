using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Damon.MVC {

    public class ViewRegister {
        private static List<IView> views = new List<IView> ();

        public  T GetView<T> (string name) where T : IView {
            T t =default(T);
            foreach (T v in views) {
                if (v.name == name) {
                    t = v;
                }
            }
            return t;
        }
        public static void Register (IView view) {
            if (!views.Contains (view)) {
                views.Add (view);
            }
        }
        public static void UnRegister (string name) {
            foreach (IView v in views) {
                if (v.name == name) {
                    views.Remove (v);
                }
            }
        }
    }
}