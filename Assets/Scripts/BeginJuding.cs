using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginJuding : MonoBehaviour {

    public static int count;
    public static float delta;
    private float XLength = 20.0f;

    void OnClick()
    {
        count = SaveData.dataList.Count;
        delta = XLength / count;
    }
}
