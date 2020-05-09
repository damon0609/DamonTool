using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Damon.Tool;

namespace Damon.Tool {

    public interface ICustomGizmos {
        bool activte { get; set; }
        bool isInit { get; set; }

        void OnDrawCustomGizmos();
        void OnInit();
    }
}
