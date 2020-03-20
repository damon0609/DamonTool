using Damon.Tool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGizmos : MonoBehaviour,IGizmos
{
    [SerializeField]
    private bool mIsShow;
    public bool isShow
    {
        set
        {
            mIsShow = value;
        }
    }

    [SerializeField]
    private Color mColor;
    public Color color { set { mColor = value; } }

    public void OnDrawGizmosItem()
    {
        if (!mIsShow||!GizmosManager.isShowGizmos) return;
        Gizmos.color = mColor;
        GizmosTool.DrawSphere(mCacheTrans.position, 0.2f);
    }

    private Transform mCacheTrans;

    void Awake()
    {
        mCacheTrans = transform;
    }

    void Start()
    {
        GizmosManager.AddGizmos(this);
    }


    void Update()
    {
        
    }
}
