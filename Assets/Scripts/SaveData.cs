using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour {

    public static List<FeatureData> dataList = new List<FeatureData>();

	// Use this for initialization
	void Start () {
		
	}
	
    public void Write()
    {
        FileStream fs = new FileStream("Data/data.txt", FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        
        for (int i = 0; i < dataList.Count; i++)
        {
            sw.Write(dataList[i].ToString());
            if (i % 100 == 0)
            {
                sw.Flush();
            }
        }
        sw.Flush();
        sw.Close();
        fs.Close();
        Debug.Log("Save data is over");
    }

	void OnClick()
    {
        Debug.Log("Begin save data");
        Write();
        //dataList.Clear();
    }
}
