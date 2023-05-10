using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SceneChange : MonoBehaviour
{
    private Image levelChange;

    private static SceneChange _instance;

    public static SceneChange Instance { get => _instance; private set => _instance = value; }

    private void Awake()
    {
        Instance = this;
        levelChange = GetComponent<Image>();
        
        levelChange.enabled = false;
        //EventCenter.AddListener(EventDefine.SceneChangeStart, OnLevelChangeStart);
        //EventCenter.AddListener(EventDefine.SceneChangeEnd, OnLevelChangeEnd);
    }
    //private void OnDestroy()
    //{
    //    //EventCenter.RemoveListener(EventDefine.SceneChangeStart, OnLevelChangeStart);
    //    //EventCenter.RemoveListener(EventDefine.SceneChangeEnd, OnLevelChangeEnd);
    //}
    private void Start()
    {
        levelChange.enabled = true;
        OnLevelChangeEnd();
    }
    private void Update()
    {
        if(levelChange.color.a<0.3f)
        {
            levelChange.maskable = false;
            levelChange.raycastTarget = false;
        }
        else
        {
            levelChange.maskable = true;
            levelChange.raycastTarget = true;
        }
    }
    public void OnLevelChangeStart()
    {
        levelChange.enabled = true;
        //levelChange.color = new Color(1, 1, 1, 0);
        levelChange.DOFade(1f, 1f).SetUpdate(true);
    }
    private void OnLevelChangeEnd()
    {

        StartCoroutine(DelayShow());
    }
    IEnumerator DelayShow()
    {
        yield return null;
        levelChange.DOFade(0, 1f).OnComplete(
            () => { levelChange.enabled = false; }).SetUpdate(true);
    }
}
