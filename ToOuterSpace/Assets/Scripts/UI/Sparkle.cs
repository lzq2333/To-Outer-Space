using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Sparkle : MonoBehaviour
{
    [SerializeField]
    private float distance = 0.1f;
    [SerializeField]
    [Range(0f,1f)]
    private float fade = 0.2f;
    [SerializeField]
    private float timer = 0.8f;
    private void Start()
    {
        Change();
    }
    /// <summary>
    /// ±ä»¯
    /// </summary>
    private void Change()
    {
        transform.DOMoveY(transform.position.y - distance, timer).SetUpdate(true);
        GetComponent<Image>().DOFade(fade, timer).SetUpdate(true).OnComplete(()=> { Recover(); });
    }
    /// <summary>
    /// »Ö¸´
    /// </summary>
    private void Recover()
    {
        transform.DOMoveY(transform.position.y + distance, timer).SetUpdate(true);
        GetComponent<Image>().DOFade(1, timer).SetUpdate(true).OnComplete(() => { Change(); });
    }
}
