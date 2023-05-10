using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonManger : MonoBehaviour
{
    private SecondTitle secondTitle;
    [SerializeField]
    private int needStarCount = 5;
    private void Awake()
    {
        secondTitle = GetComponent<SecondTitle>();
    }
    private void Start()
    {
        
    }
    void Update()
    {
        if (GameManger.Instance)
        {
            secondTitle.canBeSee = (GameManger.Instance.totalStarCount >= needStarCount);
        }
    }
    public int GetNeedCount()
    {
        return needStarCount;
    }
}
