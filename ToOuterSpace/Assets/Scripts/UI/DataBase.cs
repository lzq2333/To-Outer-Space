using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum MainTitleType
{
    /// <summary>
    /// UI介绍
    /// </summary>
    UITip,
    /// <summary>
    /// 教程视频
    /// </summary>
    VideoTip,
    /// <summary>
    /// 宇宙，物理
    /// </summary>
    Knowledge,
    /// <summary>
    /// 人文
    /// </summary>
    Person,
}
public class DataBase : MonoBehaviour
{
    
    private static DataBase _instacne;
    public static DataBase Instacne { get => _instacne; private set => _instacne = value; }

    //属性
    /// <summary>
    /// 主词条数量
    /// </summary>
    [Header("主词条数量")]
    public int mainTitleCount = 1;

    /// <summary>
    /// 主词条父级
    /// </summary>
    public Transform mainTitleParent;

    public Transform target;

    public List<Toggle> mainTitles = new List<Toggle>();

    public Transform smallerPosotion;
    private void Awake()
    {
        if (Instacne == null)
        {
            Instacne = this;
            //DontDestroyOnLoad(gameObject);
            for (int i = 0; i < mainTitleParent.childCount; i++)
            {
                if (i >= mainTitleCount)
                {
                    mainTitleParent.GetChild(i).GetComponent<Toggle>().graphic.gameObject.SetActive(false);
                    mainTitleParent.GetChild(i).gameObject.SetActive(false);
                    continue;
                }
                mainTitles.Add(mainTitleParent.GetChild(i).GetComponent<Toggle>());
                mainTitles[i].GetComponentInChildren<Text>().text = "关卡 " + (i + 1);

            }
            transform.localScale = Vector3.zero;
            
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.localScale == Vector3.zero)
        {
            if (Once.Instance)
            {
                if(target!=null)
                    transform.position = target.position;//DOMove(new Vector3(250, 1000, 0), 0.001f).SetUpdate(true);
            }
            else
            {
                transform.localPosition = Vector3.zero;
            }
        }

    }
    public void Show()
    {
        //gameObject.SetActive(true);
        //transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 0.8f).SetUpdate(true);
        if (Once.Instance != null)
        {
            transform.DOMove(smallerPosotion.position, 0.7f).SetUpdate(true);
            
        }
        else
        {
            transform.localPosition = Vector3.zero;
        }    
        Time.timeScale = 0;
    }
    public void Close()
    {
        transform.DOScale(Vector3.zero, 0.8f).SetUpdate(true);
        if (Once.Instance != null)
        {
            if(target!=null)
                transform.DOMove(target.position, 0.7f).SetUpdate(true);
        }
        
        
        if (!Pause.Instance || !Pause.Instance.IsPause)
            Time.timeScale = 1;
    }
    public void UpdateKnowledgeMangers()
    {
        GameManger.Instance.knowledgeMangers.Clear();
        Transform temp = transform.Find("SecondTitles/Main0 (2)/Content (2)").transform;

        List<KnowledgeInfo> knowledgeInfos = new List<KnowledgeInfo>();
        for (int i = 0; i < temp.parent.GetComponent<SecondTitleManger>().secondTitleCount; i++)
        {
            GameManger.Instance.knowledgeMangers.Add(temp.GetChild(i).GetComponent<KnowledgeManger>());
            KnowledgeInfo info = new KnowledgeInfo();
            info.knowledgeMangerID = GameManger.Instance.knowledgeMangers[i].GetID();
            info.isGet = GameManger.Instance.knowledgeMangers[i].isGet;
            knowledgeInfos.Add(info);
        }
        if(GameManger.Instance.knowledgeInfos.Count==0)
            GameManger.Instance.UpdateKnowledgeInfos(knowledgeInfos);
    }
}
