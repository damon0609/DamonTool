using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Damon.FSM {
    [System.Serializable]
    public class FSM {
        List<BaseState> states;
        private BaseState m_PreState;

        [SerializeField]
        public BaseState m_CurState;

        public string name;
        public BaseState curState {
            get {
                if (null != m_CurState)
                    return m_CurState;
                else
                    return null;
            }
        }

        public void ChangeState (BaseState state) {
            if (m_CurState != null) {
                m_PreState = m_CurState;
                m_CurState.Exit ();
                state.Enter ();
                m_CurState = state;
            }
        }

        public FSM () {
            states = new List<BaseState> ();
        }

        public void ExitFSM () {
            states.Clear ();
            m_PreState = m_CurState = null;
        }

        public void EntryState (BaseState state) {
            if (m_CurState != null) {
                m_CurState.Exit ();
            } else {
                this.m_CurState = state;
                m_CurState.Enter ();

            }
        }

        public BaseState AddState (BaseState state) {
            if (states != null && !states.Contains (state)) {
                states.Add (state);
            }
            return state;
        }

        public BaseState RemoveState (BaseState state) {
            if (states.Contains (state)) {
                states.Remove (state);
            }
            return state;
        }

        public void Update () {
            if (m_CurState != null)
                m_CurState.Update ();
        }
    }
}