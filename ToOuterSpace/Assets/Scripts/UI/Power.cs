using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Power : MonoBehaviour
{
    private static Power _instance;

    public static Power Instance { get => _instance; private set => _instance = value; }
    private Text txt_count;
    private Image img_fill;
    /// <summary>
    /// 最大燃料
    /// </summary>
    private int maxCount;
    /// <summary>
    /// 当前填充度
    /// </summary>
    private float currentFill=1;
    /// <summary>
    /// 填充速度
    /// </summary>
    public float fillSpeed = 3;
    private void Awake()
    {
        Instance = this;
        txt_count = transform.Find("Txt_Count").GetComponent<Text>();
        img_fill = transform.Find("PowerLine/Fill").GetComponent<Image>();
        
    }
    private void Start()
    {
        maxCount = Shuttle.shuttle.jet;
    }
    private void Update()
    {
        if (Shuttle.shuttle != null)
        {
            txt_count.text = "X " + Shuttle.shuttle.jet;
            currentFill = Mathf.Clamp(Shuttle.shuttle.jet * 1.0f / maxCount, 0, 1);
        }
        
        if(img_fill.fillAmount!=currentFill)
        {
            img_fill.fillAmount = Mathf.Lerp(img_fill.fillAmount, currentFill, fillSpeed * Time.unscaledDeltaTime);
        }
    }
}
