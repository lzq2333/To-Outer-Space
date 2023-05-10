using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTitle : MonoBehaviour
{
    /// <summary>
    /// ����Ŀ����
    /// </summary>
    public MainTitleType titleType;

    private Toggle mainTitleToggle;
    /// <summary>
    /// ��Ӧ�ĸ�����
    /// </summary>
    public List<Toggle> secondTitles = new List<Toggle>();
    // ���� +��UI���ܣ�+���̳̣� 

    public SecondTitleManger secondTitleManger;

    
    private void Awake()
    {

        
        

        mainTitleToggle = GetComponent<Toggle>();
        mainTitleToggle.onValueChanged.AddListener(MainTitleChange);
        secondTitleManger = mainTitleToggle.graphic.transform.GetComponent<SecondTitleManger>();
        //level = int.Parse(gameObject.name.Remove(0, 9));

        //û�н̳�
        //if(UIIntroduce.GetUIIntroduce().videoTips[level].videoClip.Length==0)
        //{
        //    secondTitleCount--;
        //}
        //û��UI����
        
        //if (UIIntroduce.GetUIIntroduce().uIIntroductionInfos[level].introduce==null || UIIntroduce.GetUIIntroduce().uIIntroductionInfos[level].introduce.
        //    transform.Find("Img_ContentPanel").childCount==1)
        //{
        //    secondTitleCount--;
        //}

        
    }
    private void Start()
    {
        string name = "δ����";
        switch (titleType)
        {
            case MainTitleType.UITip:
                name = "��Ϸ����";
                break;
            case MainTitleType.VideoTip:
                name = "��Ƶ�̳�";
                break;
            case MainTitleType.Knowledge:
                name = "����֪ʶ";
                break;
            case MainTitleType.Person:
                name = "����";
                break;
        }
        GetComponentInChildren<Text>().text = name;

        MainTitleChange(mainTitleToggle.isOn);
    }
    private void Update()
    {
        MainTitleChange(mainTitleToggle.isOn);
    }
    private void MainTitleChange(bool isOn)
    {
        if(isOn)
        {
            transform.localScale = Vector3.one * 1.2f;
            GetComponentInChildren<Image>().color = GameManger.Instance.mainTitleColor;
            mainTitleToggle.graphic.transform.GetChild(0).gameObject.SetActive(true);
            
        }
        else
        {
            transform.localScale = Vector3.one;
            GetComponentInChildren<Image>().color = Color.white;
            mainTitleToggle.graphic.transform.GetChild(0).gameObject.SetActive(false);
            
        }
    }
}
