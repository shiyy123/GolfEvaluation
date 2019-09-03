using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRecording : MonoBehaviour {

    /// <summary>
    /// 关闭窜口
    /// </summary>
    private void ClosePort()
    {
        try
        {
            if (Rotate.sp.IsOpen)
            {
                Rotate.sp.Close();
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    void OnClick()
    {
        ClosePort();
        //collect the rubbish
        Rotate.buffer.Clear();
        Rotate.buffer.Clear();
        Rotate.dataQueue.Clear();
        GC.Collect();
        Rotate.receiveThread.Abort();
        Rotate.processThread.Abort();

    }
}
