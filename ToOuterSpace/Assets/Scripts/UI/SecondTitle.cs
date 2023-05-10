using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondTitle : MonoBehaviour
{
    /// <summary>
    /// 对应的主条目
    /// </summary>
    public MainTitleType titleType;

    /// <summary>
    /// 玩家是否能看到
    /// </summary>
    public bool canBeSee = false;

    private Toggle myToggle;

    /// <summary>
    /// 显示的父物体
    /// </summary>
    public Transform viewContentParent;

    /// <summary>
    /// 对应的预制体
    /// </summary>
    [Header("对应的预制体")]
    public GameObject myContent;
    /// <summary>
    /// 条件未达成的显示物体
    /// </summary>
    [Header("条件未达成的显示物体")]
    public GameObject noContent;

    private GameObject grahicGo;
    private bool isNoSeeGo=true;

    /// <summary>
    /// 对应的关卡
    /// </summary>
    [SerializeField]
    private int level = 0;

    private KnowledgeManger knowledgeManger;
    private void Awake()
    {
        knowledgeManger = GetComponent<KnowledgeManger>();
        myToggle = GetComponent<Toggle>();
        myToggle.onValueChanged.AddListener(OnToggleValueChanged);
        noContent = UIIntroduce.GetUIIntroduce().lockDataBase;
        GetComponentInChildren<Image>().sprite = UIIntroduce.GetUIIntroduce().secondTitleImg;
        transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponentInChildren<RectTransform>().sizeDelta.x+30, 80);
        GetComponentInChildren<Text>().color = Color.white;
        if (titleType != MainTitleType.Knowledge && titleType != MainTitleType.Person)
        {
            GetComponentInChildren<Text>().fontSize = 30;
        }
        
        OnEnable();
        
    }
    private void OnEnable()
    {
        if(GameManger.Instance==null)
        {
            return;
        }
        if(DataBase.Instacne==null)
        {
            return;
        }
        if(myToggle.isOn)
        {
            myToggle.graphic?.gameObject.SetActive(true);
        }
        switch(titleType)
        {
            case MainTitleType.UITip:
            case MainTitleType.VideoTip:
                if(!GameManger.Instance.sceneDic.ContainsKey(level))
                {
                    break;
                }
                if(!GameManger.Instance.sceneDic[level])
                {
                    canBeSee = true;
                }
                else
                {
                    canBeSee = false;
                }
                break;
            case MainTitleType.Knowledge:
                
                canBeSee = GameManger.Instance.isGetKnowledegByID(knowledgeManger.GetID());
                
                break;
            case MainTitleType.Person:
                
                break;
        }
        //OnToggleValueChanged(myToggle.isOn);
    }
    private void Start()
    {
        //OnToggleValueChanged(false);
        if (GameManger.Instance == null)
        {
            return;
        }
        
        switch (titleType)
        {
            case MainTitleType.UITip:
            case MainTitleType.VideoTip:
                if (!GameManger.Instance.sceneDic.ContainsKey(level))
                {
                    break;
                }
                if (!GameManger.Instance.sceneDic[level])
                {
                    canBeSee = true;
                }
                else
                {
                    canBeSee = false;
                }
                break;
            case MainTitleType.Knowledge:
                canBeSee = GameManger.Instance.isGetKnowledegByID(knowledgeManger.GetID());
                break;
            case MainTitleType.Person:
                
                break;
        }

    }
    private void OnDisable()
    {
        OnToggleValueChanged(false);
    }
    private void Update()
    {
        OnToggleValueChanged(myToggle.isOn);
        Start();
        if (DataBase.Instacne.transform.localScale == Vector3.zero)
        {
            OnDisable();
        }
        //if(titleType==MainTitleType.Person)
        //{
        //    if(GetComponent<PersonManger>().GetNeedCount()<=0)
        //    {
        //        canBeSee = true;                
        //        OnToggleValueChanged(myToggle.isOn);
        //    }
        //}
        
    }
    private void OnToggleValueChanged(bool isOn)
    {
        if(isOn)
        {
            GetComponentInChildren<Image>().color = GameManger.Instance.secondTitleColor;
            gameObject.transform.localScale = Vector3.one * 1.2f;
            if(myToggle.graphic!=null)
            {
                myToggle.graphic.gameObject.SetActive(true);
            }
            if (canBeSee)
            {
                if(myToggle.graphic==null)
                {
                    grahicGo = Instantiate(myContent, viewContentParent);
                    myToggle.graphic=grahicGo.GetComponent<Image>();
                    isNoSeeGo = false;
                }
                else if(isNoSeeGo)
                {
                    Destroy(grahicGo);
                    grahicGo = Instantiate(myContent, viewContentParent);
                    myToggle.graphic = grahicGo.GetComponent<Image>();
                    isNoSeeGo = false;
                }
                   
                
            }
            else
            {
                
                if (myToggle.graphic == null)
                {
                    grahicGo = Instantiate(noContent, viewContentParent);
                    myToggle.graphic = grahicGo.GetComponent<Image>();
                    isNoSeeGo = true;
                    
                }
                else if(!isNoSeeGo)
                {
                    Destroy(grahicGo);
                    grahicGo = Instantiate(noContent, viewContentParent);
                    myToggle.graphic = grahicGo.GetComponent<Image>();
                    isNoSeeGo = true;
                }
                if (titleType == MainTitleType.Person)
                {
                    if(GameManger.Instance.currentLanguage=="cns")
                    {
                        grahicGo.GetComponentInChildren<Text>().text = "解锁本条还需要 " +
                            (GetComponent<PersonManger>().GetNeedCount() - GameManger.Instance.totalStarCount)+" 颗星星";
                        }
                    else
                    {
                        grahicGo.GetComponentInChildren<Text>().text = "To UnLock The Tip, You Need More " +
                            (GetComponent<PersonManger>().GetNeedCount() - GameManger.Instance.totalStarCount) + " Stars";
                    }
                }
                else
                {
                    if (GameManger.Instance.currentLanguage == "cns")
                    {


                        grahicGo.GetComponentInChildren<Text>().text = "本内容尚未解锁，请继续旅程吧！！！";
                    }
                    else
                    {
                        grahicGo.GetComponentInChildren<Text>().text = "The Content Haven't Been Unlock, Please Continue Your Journey!!!";
                    }
                }
            }
        }
        else
        {
            GetComponentInChildren<Image>().color = Color.white;
            gameObject.transform.localScale = Vector3.one;
            myToggle.graphic?.gameObject.SetActive(false);
        }
    }
    
}
