using UnityEngine;
using Damon.Tool;
public class TestCustomGizmos: MonoBehaviour,ICustomGizmos
{
    #region Gizmos
    private bool mActive = false;
    public bool activte
    {
        get
        {
            return mActive;
        }
        set
        {
            mActive = value;
        }
    }
    private bool mIsInit = false;
    public bool isInit
    {
        get { return mIsInit; }
        set { mIsInit = value; }
    }

    public void OnInit()
    {
        if (!mIsInit)
        {
            mIsInit = true;
            GizmosManager.AddGizmos(this);
            //BaseShape baseShape = null;
            //GizmosManager.AddShape(baseShape);
        }
    }
    public void OnDrawCustomGizmos()
    {
        OnInit();
    }
    #endregion
}
