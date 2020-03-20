using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Damon.FSM;
public class TestFSM : MonoBehaviour
{
    public FSM fsm;
    private BaseState idle;
    private BaseState run;
    private BaseState attack;
    private BaseState damage;
    private BaseState dead;

    private float idleTimer = 20.0f;
    void Start()
    {
        fsm = new FSM();
        idle = new Idle(StateType.Idle, () =>
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("待机==>奔跑left");
                fsm.ChangeState(run);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("待机==>攻击");
                fsm.ChangeState(attack);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("待机==>伤害");
                fsm.ChangeState(damage);
            }
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                Debug.Log("待机==>死亡");
                fsm.ChangeState(dead);
            }
        });
        run = new Run(StateType.Run, ()=> {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("奔跑==>攻击");
                fsm.ChangeState(attack);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("奔跑==>伤害");
                fsm.ChangeState(damage);
            }
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                Debug.Log("奔跑==>死亡");
                fsm.ChangeState(dead);
            }
        });
        attack = new Attack(StateType.Attack, ()=> {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("攻击==>伤害");
                fsm.ChangeState(damage);
            }
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                Debug.Log("攻击==>死亡");
                fsm.ChangeState(dead);
            }
        });
        damage = new Damage(StateType.Damage, null);
        dead = new Dead(StateType.Dead, null);

        fsm.AddState(idle);
        fsm.AddState(run);
        fsm.AddState(attack);
        fsm.AddState(damage);
        fsm.AddState(dead);

        fsm.EntryState(idle);
    }

    void Update()
    {
        fsm.Update();
    }
}
