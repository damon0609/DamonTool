using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Damon.FSM
{
    public enum StateType
    {
        Idle,
        Run,
        Attack,
        Damage,
        Dead,
    }

    public abstract class StateTrigger
    {

    }

    public class RunStateTrigger : StateTrigger
    {
        public RunStateTrigger(BaseState state)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("待机==>攻击");
            }
        }
    }

    public abstract class BaseState : IState
    {
        [SerializeField]
        public StateType mStateType;
        private List<Action> list = new List<Action>();
        public Action mAction;
        public BaseState(StateType type, Action action)
        {
            this.mStateType = type;
            this.mAction = action;
            list.Add(mAction);
        }

        public void RegisterState(BaseState baseState)
        {
            list.Add(baseState.mAction);
        }

        public virtual void Enter()
        {
            Debug.Log("enter==>" + mStateType.ToString());
        }

        public virtual void Update()
        {
            foreach (Action action in list)
            {
                if (action != null)
                    action();
            }
        }

        public virtual void Exit()
        {
            Debug.Log("exit==>" + mStateType.ToString());
        }
    }
}
