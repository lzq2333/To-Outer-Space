using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoTipManger : MonoBehaviour
{
    /// <summary>
    /// 视频播放器
    /// </summary>
    public VideoPlayer videoPlayer;
    /// <summary>
    /// 要播放的视频
    /// </summary>
    public List<VideoClip> videoClips = new List<VideoClip>();
    public List<string> txt_videoTips=new List<string>();
    /// <summary>
    /// 当前Clip
    /// </summary>
    private int currentClip;

    [SerializeField]
    [Header("显示的文本")]
    private Text txt_show;

    [SerializeField]
    [Header("显示用的点")]
    private GameObject point;

    [SerializeField]
    [Header("点的父物体")]
    private Transform pointParent;

    public Button btn_play;

    private Image img_Bg;
    /// <summary>
    /// 对应关卡
    /// </summary>
    public int level = 1;

    private GameObject btn_last;
    private GameObject btn_next;

    private void Awake()
    {
        txt_videoTips = UIIntroduce.GetUIIntroduce().videoTips[level-1].txt_videoTips;
        point.GetComponent<Image>().color = Color.green;
        currentClip = 0;
        btn_play.onClick.AddListener(PlayVideo);
        img_Bg = transform.GetChild(1).GetComponent<Image>();

        btn_last = transform.Find("Btn_Last").gameObject;
        btn_next = transform.Find("Btn_Next").gameObject;
        btn_last.SetActive(false);
    }
    private void Start()
    {
        videoPlayer.clip = videoClips[0];
        txt_show.text = txt_videoTips[0];
        for (int i = 0; i < videoClips.Count - 1; i++)
        {
            Instantiate(point, pointParent).GetComponent<Image>().color = Color.white;
        }
    }
    
    /// <summary>
    /// 播放视频
    /// </summary>
    public void PlayVideo()
    {

        btn_play.gameObject.SetActive(false);
        img_Bg.enabled = false;
        videoPlayer.Play();

    }
    private void OnEnable()
    {
        btn_play.gameObject.SetActive(true);
        
        StopVideo();
    }
    void OnDisable()
    {
        StopVideo();
    }
    public void StopVideo()
    {
        img_Bg.enabled = true;
        pointParent.GetChild(currentClip).GetComponent<Image>().color = Color.white;
        currentClip = 0;
        
        videoPlayer.clip = videoClips[0];
        txt_show.text = txt_videoTips[0];
        point.GetComponent<Image>().color = Color.green;
        videoPlayer.Stop();
    }
    public void OnNextButtonClick()
    {
        if (currentClip < videoClips.Count - 1)
        {
            //print(currentClip);
            //print(clips.Length - 1);
            //print(txt_videoTips.Count);
            pointParent.GetChild(currentClip).GetComponent<Image>().color = Color.white;
            currentClip++;
            pointParent.GetChild(currentClip).GetComponent<Image>().color = Color.green;
            videoPlayer.clip = videoClips[currentClip];
            txt_show.text = txt_videoTips[currentClip];
            if (!btn_play.gameObject.activeSelf)
            {
                videoPlayer.Play();
            }
        }
        btn_last.SetActive(true);
        if(currentClip == videoClips.Count - 1)
        {
            btn_next.SetActive(false);
        }
    }
    public void OnLastButtonClick()
    {
        if (currentClip > 0)
        {
            pointParent.GetChild(currentClip).GetComponent<Image>().color = Color.white;
            currentClip--;
            pointParent.GetChild(currentClip).GetComponent<Image>().color = Color.green;
            videoPlayer.clip = videoClips[currentClip];
            txt_show.text = txt_videoTips[currentClip];
            if (!btn_play.gameObject.activeSelf)
            {
                videoPlayer.Play();
            }
        }
        btn_next.SetActive(true);
        if(currentClip==0)
        {
            btn_last.SetActive(false);
        }
    }
}
