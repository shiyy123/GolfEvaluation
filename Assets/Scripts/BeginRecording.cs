using UnityEngine;
using System.IO.Ports;
using System;
using System.Threading;

public class BeginRecording : MonoBehaviour {

    private string portName;
    private int baudRate;
    private Parity parity;
    private int dataBits;
    private StopBits stopBits;

    /// <summary>
    /// 初始化串口信息,并开启接收、处理数据线程
    /// </summary>
    void Start()
    {
        portName = "COM3";
        baudRate = 115200;
        parity = Parity.None;
        dataBits = 8;
        stopBits = StopBits.One;

        Rotate.receiveThread = new Thread(new ThreadStart(receiveData));
        Rotate.receiveThread.IsBackground = true;
        Rotate.receiveThread.Start();

        Rotate.processThread = new Thread(new ThreadStart(processData));
        Rotate.processThread.IsBackground = true;
        Rotate.processThread.Start();
    }

    /// <summary>
    /// 接收数据线程
    /// </summary>
    protected void receiveData()
    {
        byte[] dataBytes = new byte[1024];
        int bytesToRead = 0;

        while (true)
        {
            if (null != Rotate.sp && Rotate.sp.IsOpen)
            {
                try
                {
                    bytesToRead = Rotate.sp.Read(dataBytes, 0, dataBytes.Length);
                    lock (Rotate.buffer)
                    {
                        for (int i = 0; i < bytesToRead; i++)
                        {
                            Rotate.buffer.Add(dataBytes[i]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                }
            }
        }
    }

    /// <summary>
    /// 数据处理线程，从串口接收数据函数的buffer中读出数据并处理
    /// </summary>
    protected void processData()
    {
        while (true)
        {
            if (Rotate.buffer.Count > 24)
            {
                if (Rotate.buffer[0] == 0x80 
                    && (Rotate.buffer[1] == 0x0A || Rotate.buffer[1] == 0x0B || Rotate.buffer[1] == 0x0C || Rotate.buffer[1] == 0x0D || Rotate.buffer[1] == 0x0E))
                {
                    PacketData tempPacket = new PacketData();
                    for (int i = 0; i < 24; i++)
                    {
                        tempPacket.data[i] = Rotate.buffer[i];
                    }

                    lock (Rotate.dataQueue)
                    {
                        Rotate.dataQueue.Enqueue(tempPacket);
                    }

                    lock (Rotate.buffer)
                    {
                        Rotate.buffer.RemoveRange(0, 23);
                    }
                }
                else
                {
                    lock (Rotate.buffer)
                    {
                        Rotate.buffer.RemoveAt(0);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 打开串口
    /// </summary>
    private void OpenPort()
    {
        Rotate.sp = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
        Rotate.sp.ReadTimeout = 500;
        try
        {
            if (!Rotate.sp.IsOpen)
            {
                Rotate.sp.Open();
                Rotate.sp.DiscardInBuffer();
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
	
    void OnClick()
    {
        OpenPort();
    }
}
