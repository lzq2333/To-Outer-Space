using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[AddComponentMenu("Scene Change By Anim")]
public class SceneChangeByAnim : MonoBehaviour
{
    private Animator anim;
    private int openView = Animator.StringToHash("Open");
    private int closeView = Animator.StringToHash("Close");
    private int updown = Animator.StringToHash("UpDown");
    private static SceneChangeByAnim _instance;
    public static SceneChangeByAnim Instance { get => _instance; private set => _instance = value; }

    private void Awake()
    {
        Instance = this;
        anim = GetComponent<Animator>();
        anim.updateMode = AnimatorUpdateMode.UnscaledTime;
        
    }
    private void Start()
    {
        OpenView();
    }
    public void OpenView()
    {
        anim.SetBool(openView,true);
        Time.timeScale = 0;
    }
    public void FadeBG()
    {
        Destroy(transform.GetChild(3).gameObject);
        Destroy(transform.GetChild(2).gameObject);
        anim.SetBool(updown, true);
    }
    //public void CloseView()
    //{
    //    anim.SetBool(closeView,true);
    //}
    public void DestoryAnim()
    {
        if(GameObject.Find("Level/Canvas/UITip").transform.childCount==0)
        {
            Time.timeScale = 1;
        }
        Destroy(gameObject);
    }
}
