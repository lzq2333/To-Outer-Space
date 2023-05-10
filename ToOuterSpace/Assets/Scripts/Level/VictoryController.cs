using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryController : MonoBehaviour
{
    
    public bool isIn;
    
    public bool isOut;

    /// <summary>
    /// ��ʱ��
    /// </summary>
    private float timer = 0;

    /// <summary>
    /// Ҫͣ����ʱ��
    /// </summary>
    [Header("���Ҫͣ����ʱ��")]
    public float staryTime = 3;

    private List<Image> images;


    private void Awake()
    {
        images = new List<Image>();
        for(int i=0;i<transform.childCount;i++)
        {
            images.Add(transform.GetChild(i).GetChild(0).GetComponent<Image>());
            
        }
    }
    private void Update()
    {
        if(Shuttle.shuttle==null)
        {
            
            return;
        }
        if(isIn && isOut)
        {            
            timer += Time.deltaTime*Shuttle.timeScale*Shuttle.shuttle.speedRate;
        }
        else
        {
            timer = 0;
        }

        for(int i=0;i<images.Count;i++)
        {
            images[i].fillAmount = Mathf.Clamp(timer / staryTime, 0, 1);
        }
        Shuttle.shuttle.Winprogress = images[0].fillAmount;
        if (timer>=staryTime)
        {
            End.EndGame(true);
        }
    }

}
