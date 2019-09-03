using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour {

    private Color c1 = Color.red;
    private Color c2 = Color.red;
    public LineRenderer lineRenderer;

    private Vector3 v0 = new Vector3(0.0f, 0.0f, 3.0f);
    private Vector3 v1 = new Vector3(2.0f, 2.0f, 0.0f);
    private Vector3 v2 = new Vector3(4.0f, 3.0f, 1.0f);
    private Vector3 v3 = new Vector3(10.0f, 4.0f, 0.0f);
    private Vector3 v4 = new Vector3(15.0f, 6.0f, 2.0f);
    private Vector3 v5 = new Vector3(20.0f, 8.6f, 0.0f);

    // Use this for initialization
    void Start () {
        lineRenderer.startColor = c1;
        lineRenderer.endColor = c2;
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
        lineRenderer.numPositions = 6 ;
    }
	
	// Update is called once per frame
	void Update () {
        lineRenderer.SetPosition(0, v0);
        lineRenderer.SetPosition(1, v1);
        lineRenderer.SetPosition(2, v2);
        lineRenderer.SetPosition(3, v3);
        lineRenderer.SetPosition(4, v4);
        lineRenderer.SetPosition(5, v5);
    }
}
