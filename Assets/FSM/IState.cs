using System;
namespace Damon.FSM {
    public interface IState {
        void Enter ();
        void Update ();
        void Exit ();
    }
}