using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setTxt : MonoBehaviour {

    private UILabel label;
	// Use this for initialization
	void Start () {
        label = GetComponent<UILabel>();
	}
	
	// Update is called once per frame
	void Update () {
        string txt = "abcdefd";
        label.text = txt;
	}
}
