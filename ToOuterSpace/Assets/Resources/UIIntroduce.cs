using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

//[CreateAssetMenu(fileName ="UIIntroduction",menuName ="Create UIIntroduction")]
public class UIIntroduce : ScriptableObject
{
    [System.Serializable]
    public struct UIIntroductionInfo
    {
        public int level;
        public GameObject introduce;
    }
    
    
    public UIIntroductionInfo[] uIIntroductionInfos;
    public Sprite[] levelButtons;
    public VideoInfo[] videoTips;

    public GameObject lockDataBase;

    public Sprite secondTitleImg;
    //[SerializeField]
    //public string[][] txt_videoTips;
    public static UIIntroduce GetUIIntroduce()
    {
        return Resources.Load<UIIntroduce>("UIIntroduction");
    }
    
}
[System.Serializable]
public struct VideoInfo
{
    public int level;
    public VideoClip[] videoClip;
    public List<string> txt_videoTips;
}