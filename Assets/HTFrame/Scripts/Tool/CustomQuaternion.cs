using System;
using System.Collections;
using System.Collections.Generic;
using Damon;
using UnityEngine;

/*
* 四元数
* 相等性检查
* 相关运算的
*/
[System.Serializable]
public class CustomQuaternion : IEquatable<CustomQuaternion> {

    private float mX;
    public float x {
        get { return mX; }
        set { mX = value; }
    }
    private float mY;
    public float y {
        get { return mY; }
        set { mY = value; }
    }
    private float mZ;
    public float z {
        get { return mZ; }
        set { mZ = value; }
    }
    private float mW;
    public float w {
        get { return mW; }
        set { mW = value; }
    }
    public static readonly CustomQuaternion identiy = new CustomQuaternion (0f, 0f, 0f, 1f);
    public CustomQuaternion (float x, float y, float z, float w) {
        this.mX = x;
        this.mY = y;
        this.mZ = z;
        this.mW = w;
    }

    public bool Equals (CustomQuaternion other) {
        return x.NearFromObject (other.x) && y.NearFromObject (other.y) && z.NearFromObject (other.z) && w.NearFromObject (other.w);
    }
    public bool IsValild () {
        return !(float.IsNaN (x) || float.IsInfinity (x) ||
            float.IsNaN (y) || float.IsInfinity (y) ||
            float.IsNaN (z) || float.IsInfinity (z) ||
            float.IsNaN (w) || float.IsInfinity (w)
        );
    }

    public float Magnitude{
        get{return (float)Math.Sqrt(x*x+y*y+z*z+w*w);}
    }
    public CustomQuaternion normalized
    {
        get{
            if(Magnitude<Constants.EPSILON)
                return identiy;
            
            float rate = (float)1.0/Magnitude;
            CustomQuaternion c = new CustomQuaternion(x*rate,y*rate,z*rate,w*rate);
            return c;
        }
    }


}