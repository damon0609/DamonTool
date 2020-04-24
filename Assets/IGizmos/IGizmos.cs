using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Damon.Tool {
    public interface IGizmos {
        bool isShow { set; }
        Color color { set; }
        void OnDrawGizmosItem ();
    }

}