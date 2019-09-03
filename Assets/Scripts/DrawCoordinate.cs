using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCoordinate : MonoBehaviour {

    private Color c1 = Color.black;
    private Color c2 = Color.black;
    private LineRenderer line;

    private Vector3 v0;
    private Vector3 v1;
    private Vector3 v2;
    private Vector3 v3;

    private void Start()
    {
        //v0 = new Vector3(0.0f, 10.0f, 0.0f);
        //v1 = new Vector3(0.0f, -10.0f, 0.0f);
        //v2 = new Vector3(20.0f, -10.0f, 0.0f);
        line = GetComponent<LineRenderer>();

        v0 = new Vector3(20.0f, 0.0f, 0.0f);
        v1 = new Vector3(0.0f, 0.0f, 0.0f);
        v2 = new Vector3(0.0f, 10.0f, 0.0f);
        v3 = new Vector3(0.0f, -10.0f, 0.0f);

        line.startColor = c1;
        line.endColor = c2;
        line.startWidth = 0.2f;
        line.endWidth = 0.2f;
        line.numPositions = 4;
    }

    private void Update()
    {
        line.SetPosition(0, v0);
        line.SetPosition(1, v1);
        line.SetPosition(2, v2);
        line.SetPosition(3, v3);
    }
}
