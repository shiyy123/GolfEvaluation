using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketData
{
    public byte[] data = new byte[24];
}

public class FeatureData
{
    public Quaternion q = new Quaternion();
    public Vector3 acc = new Vector3();
    public Vector3 mag = new Vector3();

    public FeatureData(Quaternion q, Vector3 acc, Vector3 mag)
    {
        this.q = q;
        this.acc = acc;
        this.mag = mag;
    }

    public override string ToString()
    {
        return string.Format("Quat:{0}, Acc:{1}, Mag:{2}", q.ToString(), acc.ToString(), mag.ToString());
    }
}


public static class myTools
{

    public static void NormalizeQuat(ref Quaternion q)//归一化
    {
        float N = q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w;
        if (N == 1.0f)
            return;

        N = Mathf.Sqrt(N);
        // Too close to zero.
        if (N < 0.000001f)
            return;

        N = 1.0f / N;
        q.x *= N;
        q.y *= N;
        q.z *= N;
        q.w *= N;
    }

    public static void InverseQuat(ref Quaternion q)
    {
        q.x = -q.x;
        q.y = -q.y;
        q.z = -q.z;
    }

    public static float datarecover(byte ah, byte al)
    {
        float res;
        res = (float)((short)(ah << 8) | al) / (1 << 15);
        res *= 2.0f;
        return res;
    }

    /// <summary>
    /// 定点转浮点
    /// </summary>
    /// <param name="fixedValue">short型定点数</param>
    /// <returns>浮点型数</returns>
    public static float FixedToFloat(short fixedValue)
    {
        return ((float)(fixedValue) / (float)(1 << ((int)15)));
    }

    /// <summary>
    /// 将两个字节连起来组成16位short型数据
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static short Combine(byte a, byte b)
    {
        return (short)((short)((short)a << 8) | (short)b);
    }
}
