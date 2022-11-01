using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class math
{
    public static float getSineByAngle(float angle)
    {
        return Mathf.Sin(angle * Mathf.Deg2Rad);
    }

    public static float getCosByAngle(float angle)
    {
        return Mathf.Cos(angle * Mathf.Deg2Rad);
    }

    public static Vector3 setVector3AtX(Vector3 v, float x)
    {
        return new Vector3(x, v.y, v.z);
    }

    public static Vector3 setVector3AtY(Vector3 v, float y)
    {
        return new Vector3(v.x, y, v.z);
    }

    public static Vector3 setVector3AtZ(Vector3 v, float z)
    {
        return new Vector3(v.x, v.y, z);
    }
}
