using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class IntroduceDataBase : MonoBehaviour
{
    [SerializeField]
    private Button btn_closeVideo;
    [SerializeField]
    private Button btn_close;

    private void Awake()
    {
        btn_closeVideo.onClick.AddListener(OnCloseVideoClick);
        btn_close.onClick.AddListener(Close);
        transform.localScale = Vector3.zero;
    }
    private void OnCloseVideoClick()
    {
        transform.DOScale(Vector3.one, 0.5f).SetUpdate(true);
        
        Time.timeScale = 0;
    }
    private void Close()
    {
        Time.timeScale = 1;
        transform.DOScale(Vector3.zero, 0.5f).SetUpdate(true).
            OnComplete(() => { Destroy(gameObject); });
    }
}
