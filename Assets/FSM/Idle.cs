using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Damon.FSM {
    [System.Serializable]
    public class Idle : BaseState {
        public Idle (StateType type, Action action) : base (type, action) { }

        public override void Enter () {
            base.Enter ();
        }
        public override void Exit () {
            base.Exit ();
        }
    }

    public class Run : BaseState {
        public Run (StateType type, Action action) : base (type, action) { }

        public override void Enter () {
            base.Enter ();

        }

        public override void Exit () {
            base.Exit ();
        }
    }

    public class Attack : BaseState {
        public Attack (StateType type, Action action) : base (type, action) { }
    }

    public class Damage : BaseState {
        public Damage (StateType type, Action action) : base (type, action) { }
        public override void Enter () {
            base.Enter ();
        }
        public override void Exit () {
            base.Exit ();
        }
    }

    public class Dead : BaseState {
        public Dead (StateType type, Action action) : base (type, action) { }
        public override void Enter () {
            base.Enter ();
        }
        public override void Exit () {
            base.Exit ();
        }
    }
}