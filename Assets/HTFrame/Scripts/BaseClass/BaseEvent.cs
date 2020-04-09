using Damon.Tool;
using UnityEngine;

namespace HT {

  public class EntityCreateEvent : BaseEvent {

    public EntityCreateEvent () {
      this.d ("damon", "entity");
    }
    public void Fill (Entity entity) {
      this.d ("damon", "Fill");
    }
    public override void Reset () {

    }
  }

  public class LoginEvent : BaseEvent {
    private string mName;
    private string mPassword;
    public LoginEvent (string name, string password) {
      this.mName = name;
      this.mPassword = password;
    }

    public override string ToString () {
      return string.Format ("name={0},password={1}", mName, mPassword);
    }

    public override void Reset () {

    }
  }
  public class BaseEvent : IReference {
    public virtual void Reset () {

    }
  }
}