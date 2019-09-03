using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawAxis : MonoBehaviour {

    private int screenWidth;
    private int screenHeight;

    // Use this for initialization
    void Start () {
        screenHeight = Screen.height;
        screenWidth = Screen.width;
        //Debug.Log("screenHeight"+ screenHeight+ ",screenWidth" + screenWidth);
    }

    private void OnGUI()
    {
        GUIStyle textStyle = new GUIStyle();
        textStyle.fontSize = 20;
        textStyle.normal.textColor = Color.black;
        GUI.Label(new Rect(screenWidth / 2 + 300, screenHeight / 2, 30, 30), "t/s", textStyle);
        GUI.Label(new Rect(screenWidth / 2, screenHeight / 2 - 150, 30, 30), "quat.x", textStyle);
        //GUI.Label(new Rect(0, 0, 30, 30), "原点");
        //GUI.Label(new Rect(screenWidth, 0, 30, 30), "宽");
        //GUI.Label(new Rect(0, screenHeight, 30, 30), "高");
    }
}
