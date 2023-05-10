using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TempManger : MonoBehaviour
{
    private float inRange;
    private float outRange;
    private float stayTime;
    private VictoryController victoryController;
    private GameObject temp;

    private InputField input_in;
    private InputField input_out;
    private InputField input_time;
    void Update()
    {
        if(SceneManager.GetActiveScene().name=="Level10")
        {
            if(victoryController==null)
            {
                victoryController = GameObject.Find("VectorController").GetComponent<VictoryController>();
                temp = GameObject.Find("Canvas").transform.Find("Temp").gameObject;

                input_in = temp.transform.Find("InputField_In").GetComponent<InputField>();
                input_in.onValueChanged.AddListener((string value) => { inRange = float.Parse(value); });
                input_out = temp.transform.Find("InputField_Out").GetComponent<InputField>();
                input_out.onValueChanged.AddListener((string value) => { outRange = float.Parse(value); });
                input_time = temp.transform.Find("InputField_Time").GetComponent<InputField>();
                input_time.onValueChanged.AddListener((string value) => { stayTime = float.Parse(value); });
            }
            if (victoryController != null)
            {
                victoryController.staryTime = stayTime;
                victoryController.transform.GetChild(0).GetChild(0).GetComponent<VictoryLanding>();
                victoryController.transform.GetChild(1).GetChild(0).GetComponent<VictoryLaunch>();
            }
        }
    }

}
