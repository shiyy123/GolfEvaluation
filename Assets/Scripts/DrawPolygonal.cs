using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPolygonal : MonoBehaviour {

    public LineRenderer lineRenderer;
    public int id = 0;

    private Color c1 = Color.red;
    private Color c2 = Color.red;

    // Use this for initialization
    void Start () {
        lineRenderer.startColor = c1;
        lineRenderer.endColor = c2;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }

    void Update()
    {
        if (BeginJuding.count != 0)
        {
            lineRenderer.numPositions = BeginJuding.count;
            //Debug.Log(BeginJuding.count.ToString());
            float current = 0.0f;
            for (int i = 0; i < BeginJuding.count; i++)
            {
                //lineRenderer.SetPosition(i, new Vector3(current += delta, SaveData.dataList[i].q.x, 0));
                //lineRenderer.SetPosition(i, new Vector3(current, SaveData.dataList[i].q[id] * 10.0f, 0));
                //lineRenderer.SetPosition(i, new Vector3(current, SaveData.dataList[i].q.eulerAngles[id] / 36.0f, 0));
                Debug.Log(SaveData.dataList[i].q.eulerAngles[id] / 36.0f);
                current += BeginJuding.delta;
            }
        }
    }
}
