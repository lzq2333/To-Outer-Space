using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPanelManger : MonoBehaviour
{
    public Button btn_dataBase;

    void Start()
    {
        btn_dataBase.onClick.AddListener(DataBase.Instacne.Show);
    }
}
