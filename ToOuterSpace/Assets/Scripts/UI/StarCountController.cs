using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarCountController : MonoBehaviour
{
    private List<Image> stars = new List<Image>();
    /// <summary>
    /// 
    /// </summary>
    public Color disColor;
    private void Awake()
    {
        for(int i=0;i<transform.childCount;i++)
        {
            stars.Add(transform.GetChild(i).GetComponent<Image>());
        }
    }
    private void Update()
    {
        if(Shuttle.shuttle.jet*1.0f/Shuttle.shuttle.totalJet<0.5f)
        {
            stars[0].color = disColor;
        }
        if (Shuttle.shuttle.jet * 1.0f / Shuttle.shuttle.totalJet < 0.25f)
        {
            stars[1].color = disColor;
        }
    }
}
