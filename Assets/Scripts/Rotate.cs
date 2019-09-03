using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

public class Rotate : MonoBehaviour {

    //串口
    public static SerialPort sp = null;
    //读写线程
    public static Thread processThread = null;
    public static Thread receiveThread = null;

    public Transform LeftUpLeg = null;
    public Transform LeftLeg = null;
    public Transform RightUpLeg = null;
    public Transform RightLeg = null;
    public Transform RightArm = null;
    public Transform RightForeArm = null;
    public Transform LeftArm = null;
    public Transform LeftForeArm = null;
    public Transform RightHand = null;


    public static List<byte> buffer = new List<byte>(4096);

    public static Queue<PacketData> dataQueue = new Queue<PacketData>();

    public static PacketData packetData = new PacketData();


    //矫正模型

    private static Quaternion corRightUpLeg = new Quaternion();
    private static Quaternion corRightLeg = new Quaternion();
    private static Quaternion corLeftUpLeg = new Quaternion();
    private static Quaternion corLeftLeg = new Quaternion();

    private static Quaternion corRightArm = new Quaternion();
    private static Quaternion corRightForeArm = new Quaternion();
    private static Quaternion corLeftArm = new Quaternion();
    private static Quaternion corLeftForeArm = new Quaternion();

    private static Quaternion corRightHand = new Quaternion();


    /// <summary>
    /// Init the correction of the bones of the skeleton
    /// </summary>
    void Start () {
        corRightLeg = RightLeg.rotation;
        corRightUpLeg = RightUpLeg.rotation;
        corLeftLeg = LeftLeg.rotation;
        corLeftUpLeg = LeftUpLeg.rotation;
        corRightArm = RightArm.rotation;
        corRightForeArm = RightForeArm.rotation;
        corLeftArm = LeftArm.rotation;
        corLeftForeArm = LeftForeArm.rotation;

        corRightHand = RightHand.rotation;
    }

    private void ProcessNode(ref PacketData p)
    {
        Quaternion q = new Quaternion();
        q.w = myTools.FixedToFloat(myTools.Combine(p.data[4], p.data[5]));
        q.x = -myTools.FixedToFloat(myTools.Combine(p.data[6], p.data[7]));
        q.y = myTools.FixedToFloat(myTools.Combine(p.data[8], p.data[9]));
        q.z = -myTools.FixedToFloat(myTools.Combine(p.data[10], p.data[11]));

        //q.w = datarecover(p.data[4], p.data[5]);
        //q.x = datarecover(p.data[6], p.data[7]);
        //q.y = datarecover(p.data[10], p.data[11]);
        //q.z = -datarecover(p.data[8], p.data[9]);

        //NormalizeQuat(ref q);

        //Debug.Log("process: " + q.w.ToString() + " " + q.x.ToString() + " " + q.y.ToString() + " " + q.z.ToString());
        //Debug.Log("process: " + q.eulerAngles.ToString());
        //InverseQuat(ref q);


        switch (p.data[1])
        {
            case 0x0A:
                LeftUpLeg.rotation = q * corLeftUpLeg;
                break;
            case 0x0B:
                RightUpLeg.rotation = q * corRightUpLeg;
                break;
            case 0x0C:
                LeftLeg.rotation = q * corLeftLeg;
                break;
            case 0x0D:
                RightLeg.rotation = q * corRightLeg;
                break;
            default:
                Debug.Log(packetData.data[1].ToString() + "no such node");
                break;
        }
        //暂时没有计算出 加速度、磁场
        SaveData.dataList.Add(new FeatureData(q, new Vector3(), new Vector3()));
    }

    //从队列中取数据
    void FixedUpdate()
    {
        if (dataQueue.Count > 0)
        {
            //Debug.Log(dataQueue.Count);
            lock (dataQueue)
            {
                packetData = dataQueue.Dequeue();
            }
            ProcessNode(ref packetData);
        }
    }

    void OnDestroy()
    {
        GC.Collect();
    }
}
