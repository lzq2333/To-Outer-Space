using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class Tip :MonoBehaviour
{
    [SerializeField]
    private VideoPlayer videoPlayer;
    private VideoClip[] clips;
    private List<string> txt_videoTips;
    private static Tip _instance;

    [SerializeField]
    private Transform tipButton;
    /// <summary>
    /// 当前Clip
    /// </summary>
    private int currentClip;

    private GameObject btn_last;
    private GameObject btn_next;

    [SerializeField]
    [Header("显示的文本")]
    private Text txt_show;

    [SerializeField]
    [Header("显示用的点")]
    private GameObject point;

    [SerializeField]
    [Header("点的父物体")]
    private Transform pointParent; 
    public static Tip Instance { get => _instance; private set => _instance = value; }

    private void Awake()
    {
        Instance = this;
        txt_videoTips = UIIntroduce.GetUIIntroduce().videoTips[
            int.Parse(SceneManager.GetActiveScene().name.Remove(0, 5))-1].txt_videoTips;
        point.GetComponent<Image>().color = Color.green;
        currentClip = 0;

        btn_last = transform.Find("Btn_Last").gameObject;
        btn_last.SetActive(false);
        btn_next = transform.Find("Btn_Next").gameObject;
    }
    private void Start()
    {
        if (GameManger.Instance.videoTips[End.end.Level] == null)
        {
            Debug.LogError($"关卡{End.end.Level}没有视频");
        }
        clips = GameManger.Instance.videoTips[int.Parse(SceneManager.GetActiveScene().name.Remove(0, 5))];
        

        videoPlayer.clip = clips[0];
        txt_show.text = txt_videoTips[0];
        for(int i=0;i<clips.Length-1;i++)
        {
            Instantiate(point, pointParent).GetComponent<Image>().color=Color.white;
        }
    }
    /// <summary>
    /// 播放视频
    /// </summary>
    public void PlayVideo()
    {        
        
        if(clips==null)
        {
            clips = GameManger.Instance.videoTips[int.Parse(SceneManager.GetActiveScene().name.Remove(0, 5))];
        }
        videoPlayer.clip = clips[0];
        
        
        videoPlayer.Play();
        
    }
    public void PuaseVideo()
    {
        videoPlayer.Pause();
    }
    public void StopVideo()
    {
        GetComponent<RectTransform>().DOMove(tipButton.position, 0.7f).SetUpdate(true);
        pointParent.GetChild(currentClip).GetComponent<Image>().color = Color.white;
        currentClip = 0;
        //if (clips == null)
        //{
        //    clips = GameManger.Instance.videoTips[int.Parse(SceneManager.GetActiveScene().name.Remove(0, 5))];
        //}
        videoPlayer.clip = clips[0];
        txt_show.text = txt_videoTips[0];
        point.GetComponent<Image>().color = Color.green;
        videoPlayer.Stop();
    }
    public void OnNextButtonClick()
    {
        if(currentClip<clips.Length-1)
        {
            //print(currentClip);
            //print(clips.Length - 1);
            //print(txt_videoTips.Count);
            pointParent.GetChild(currentClip).GetComponent<Image>().color = Color.white;
            currentClip++;
            pointParent.GetChild(currentClip).GetComponent<Image>().color = Color.green;
            videoPlayer.clip = clips[currentClip];
            txt_show.text = txt_videoTips[currentClip];
            videoPlayer.Play();
        }
        
        btn_last.SetActive(true);
        if (currentClip ==clips.Length - 1)
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
            videoPlayer.clip = clips[currentClip];
            txt_show.text = txt_videoTips[currentClip];
            videoPlayer.Play();
        }
        btn_next.SetActive(true);
        if (currentClip == 0)
        {
            btn_last.SetActive(false);
        }
    }
}
