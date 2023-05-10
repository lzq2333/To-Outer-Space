using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTitle : MonoBehaviour
{
    /// <summary>
    /// 主条目类型
    /// </summary>
    public MainTitleType titleType;

    private Toggle mainTitleToggle;
    /// <summary>
    /// 对应的副词条
    /// </summary>
    public List<Toggle> secondTitles = new List<Toggle>();
    // 背景 +（UI介绍）+（教程） 

    public SecondTitleManger secondTitleManger;

    
    private void Awake()
    {

        
        

        mainTitleToggle = GetComponent<Toggle>();
        mainTitleToggle.onValueChanged.AddListener(MainTitleChange);
        secondTitleManger = mainTitleToggle.graphic.transform.GetComponent<SecondTitleManger>();
        //level = int.Parse(gameObject.name.Remove(0, 9));

        //没有教程
        //if(UIIntroduce.GetUIIntroduce().videoTips[level].videoClip.Length==0)
        //{
        //    secondTitleCount--;
        //}
        //没有UI介绍
        
        //if (UIIntroduce.GetUIIntroduce().uIIntroductionInfos[level].introduce==null || UIIntroduce.GetUIIntroduce().uIIntroductionInfos[level].introduce.
        //    transform.Find("Img_ContentPanel").childCount==1)
        //{
        //    secondTitleCount--;
        //}

        
    }
    private void Start()
    {
        string name = "未命名";
        switch (titleType)
        {
            case MainTitleType.UITip:
                name = "游戏内容";
                break;
            case MainTitleType.VideoTip:
                name = "视频教程";
                break;
            case MainTitleType.Knowledge:
                name = "宇宙知识";
                break;
            case MainTitleType.Person:
                name = "人文";
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
