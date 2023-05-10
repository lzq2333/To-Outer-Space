using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using LitJson;
using UnityEngine.Video;

/// <summary>
/// 不会消失的管理类
/// </summary>
public class GameManger : MonoBehaviour
{
    private static GameManger _instance;
    public static GameManger Instance { get => _instance;private set => _instance = value; }

    /// <summary>
    /// 音乐大小
    /// </summary>
    [Range(0,1)]
    private float musicVolume=0.4f;
    /// <summary>
    /// 音效大小
    /// </summary>
    [Range(0,1)]
    private float effectVolume=0.4f;

    /// <summary>
    /// 只在第一次进行声音增大
    /// </summary>
    public bool isFirst;
    public float MusicVolume { get => musicVolume; set => musicVolume = value; }
    public float EffectVolume { get => effectVolume; set => effectVolume = value; }

    
    public int level = 1;
    /// <summary>
    /// 记录通过的关卡
    /// </summary>
    public int Level { get => level; set => level = value; }
    
    /// <summary>
    /// 最大关卡数
    /// </summary>
    [Header("最大关卡数")]

    public int MaxLevel = 9;
    /// <summary>
    /// 玩家实际最大关卡
    /// </summary>
    public int playerMaxLevel = 9;

    //public float Force = 1;
    [HideInInspector]
    public AudioSource audioSourece;

    /// <summary>
    /// 记录当前语言
    /// </summary>
    public string currentLanguage;

    /// <summary>
    /// 记录关卡是否第一次游玩
    /// </summary>
    public Dictionary<int, bool> sceneDic;

    private Dictionary<int, GameObject> uiTipDic;

    public Color mainTitleColor;
    public Color secondTitleColor;

    /// <summary>
    /// 存储星星数
    /// </summary>
    //[HideInInspector]
    public List<int> stars;

    /// <summary>
    /// 星星的总数
    /// </summary>
    public int totalStarCount;

    /// <summary>
    /// 存储视频介绍
    /// </summary>
    public Dictionary<int, VideoClip[]> videoTips;

    /// <summary>
    /// KnowledgeManger对应isGet
    /// </summary>
    public List<KnowledgeInfo> knowledgeInfos = new List<KnowledgeInfo>();

    public List<KnowledgeManger> knowledgeMangers = new List<KnowledgeManger>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            //恢复数据
            Data data= LoadData();            
            currentLanguage = data.language;
            sceneDic = new Dictionary<int, bool>();
            for(int i=0;i<data.levels.Count;i++)
            {
                if(!sceneDic.ContainsKey(i+1))
                {
                    sceneDic.Add(i + 1, data.levels[i]);
                }
            }
            Level = data.level;
            stars = new List<int>(data.stars);
            totalStarCount = GetAllStarCount();
            
            knowledgeInfos = new List<KnowledgeInfo>(data.knowledgeInfos);

            
            audioSourece = GetComponent<AudioSource>();            
            DontDestroyOnLoad(gameObject);
            isFirst = true;

            uiTipDic = new Dictionary<int, GameObject>();
            for(int i=0;i<UIIntroduce.GetUIIntroduce().uIIntroductionInfos.Length;i++)
            {
                if(!uiTipDic.ContainsKey(UIIntroduce.GetUIIntroduce().uIIntroductionInfos[i].level))
                {
                    uiTipDic.Add(UIIntroduce.GetUIIntroduce().uIIntroductionInfos[i].level, 
                        UIIntroduce.GetUIIntroduce().uIIntroductionInfos[i].introduce);
                }
            }

            
            
            //EventCenter.AddListener<int>(EventDefine.SceneChange, OnSceneChange);
            

            //存储 videoTips
            videoTips = new Dictionary<int, VideoClip[]>();
            for(int i=0;i<UIIntroduce.GetUIIntroduce().videoTips.Length;i++)
            {
                if(!videoTips.ContainsKey(UIIntroduce.GetUIIntroduce().videoTips[i].level))
                {
                    videoTips.Add(UIIntroduce.GetUIIntroduce().videoTips[i].level,
                        UIIntroduce.GetUIIntroduce().videoTips[i].videoClip);
                }
            }
        }
        if(audioSourece==null)
        {
            Destroy(gameObject);
        }
        
        
    }
    
    private void OnDestroy()
    {
        //EventCenter.RemoveListener<int>(EventDefine.SceneChange, OnSceneChange);
    }
    private void Update()
    {
        LevelCloseAudioSource();
        
    }
    /// <summary>
    /// 引力比率改变
    /// </summary>
    /// <param name="value">引力比率</param>
    public void OnForceChange(string value)
    {
        //float temp = Force;
        //if(!float.TryParse(value,out Force))
        //{
        //    Force = temp;
        //}
        if(value=="11234")
        {            
            //PlayerPrefs.DeleteAll();
            //PlayerPrefs.SetString(SaveHash.Language, currentLanguage);

            for(int i=1;i<=MaxLevel;i++)
            {
                if(sceneDic.ContainsKey(i))
                {                    
                    sceneDic[i] = true;                   
                }
            }
            Level = 1;

            for(int i=0;i<MaxLevel;i++)
            {
                stars[i] = 0;
            }
            totalStarCount = 0;
            for(int i=0;i<knowledgeInfos.Count;i++)
            {
                ChangeKnowledgeIsGet(knowledgeInfos[i].knowledgeMangerID, false);
                
            }
            SaveByJson();
            SceneManager.LoadScene("Start");
        }
        else if(value=="56789")
        {
            Upgrade(MaxLevel);
            SceneManager.LoadScene("Start");
        }
    
    }
    private void LevelCloseAudioSource()
    {
        if(SceneManager.GetActiveScene().name=="Start" || SceneManager.GetActiveScene().name == "LevelSelect")
        {
            audioSourece.enabled = true;

        }
        else if(SceneManager.GetActiveScene().name != "LevelSelect")
        {
            audioSourece.enabled = false;
        }
    }

    /// <summary>
    /// 升关
    /// </summary>
    /// <param name="level">下一个</param>
    /// <param name="starNum">本关星星数量</param>
    public void Upgrade(int level, int starNum = 0)
    {
        Level = Mathf.Clamp(level, Level, MaxLevel);
        //PlayerPrefs.SetInt(SaveHash.Level, Level);
        SaveByJson();
        if (stars[level - 2] < starNum)
        {
            stars[level - 2] = starNum;
            SaveByJson();
        }
        totalStarCount = GetAllStarCount();

    }
    /// <summary>
    /// 场景切换时如果为非0，则为跳转到游戏场景，要判断是否是第一次玩，如果是第一次玩则要判断是否加载 背景介绍面板
    /// </summary>
    /// <param name="level">等级</param>
    public void OnSceneChange(int level)
    {
        
        switch(level)
        {
            case 0:
                break;
            case 1:
                if(!sceneDic[3])
                {                    
                    GameObject.Find("Level/Canvas/LeftTopPanel/Pause").gameObject.SetActive(true);
                    //GameObject.Find("Level/Canvas/LeftTopPanel/Speed").gameObject.SetActive(true);
                }                  
                break;
            case 2:
                if (!sceneDic[3])
                {
                    GameObject.Find("Level/Canvas/LeftTopPanel/Pause").gameObject.SetActive(true);
                    //GameObject.Find("Level/Canvas/LeftTopPanel/Speed").gameObject.SetActive(true);
                }
                break;            
            default:
                break;
        }
        
        OnGameStart(level);
    }
    /// <summary>
    /// 场景切换时如果为非0，则为跳转到游戏场景，要判断是否是第一次玩，如果是第一次玩则要判断是否加载 UI介绍面板
    /// </summary>
    /// <param name="level">等级</param>
    private void OnGameStart(int level)
    {
        if(0==level)
        {
            return;
        }
        //PlayerPrefs.SetString(SaveHash.Level_ + level, "false");
        
        
        if (!sceneDic[level])
        {            
            return;          
        }
        if(null==uiTipDic[level])
        {
            sceneDic[level] = false;
            SaveByJson();
            return;
        }
        //UI介绍面板        
        Instantiate(uiTipDic[level], GameObject.Find("Level/Canvas/UITip").GetComponent<RectTransform>());
        sceneDic[level] = false;
        SaveByJson();
        
        

    }

    /// <summary>
    /// 创建Data对象并存储当前游戏状态信息
    /// </summary>
    /// <returns>带有当前游戏信息的Data对象</returns>
    private Data CreateDataGo()
    {
        Data data = new Data();
        
        
        if (stars!=null)
            data.stars = new List<int>(stars);
        else
        {
            for (int i = 0; i < MaxLevel; i++)
            {
                data.stars.Add(0);
                
            }
        }
        
        data.level = Level;
        data.language = currentLanguage;
        
        if (sceneDic != null)
        {
            data.levels.Clear(); 
            for (int i = 0; i < sceneDic.Count; i++)
            {
                               
                data.levels.Add(sceneDic.ContainsKey(i + 1) ? sceneDic[i + 1] : true);
                
            }
        }
        else
        {
            for (int i = 0; i < MaxLevel; i++)
            {
                
                data.levels.Add(true);
            }
        }
        if (knowledgeInfos.Count == 0)
        {
            for (int i = 0; i < knowledgeMangers.Count; i++)
            {
                KnowledgeInfo knowledgeInfo = new KnowledgeInfo();
                
                knowledgeInfo.isGet = false;
                knowledgeInfo.knowledgeMangerID = knowledgeMangers[i].GetID();
                data.knowledgeInfos.Add(knowledgeInfo);

            }
        }
        else
        {
            data.knowledgeInfos = new List<KnowledgeInfo>(knowledgeInfos);
        }

        return data;
    }

    /// <summary>
    /// 存数据
    /// </summary>
    public void SaveByJson()
    {
        Data data = CreateDataGo();
        
        if (!Directory.Exists(Application.dataPath+"/StreamFile"))
        {
            Directory.CreateDirectory(Application.dataPath + "/StreamFile");
            
        }
        string filePath = Application.dataPath + "/StreamFile" + "/DataByJson.json";
        if(!File.Exists(filePath))
        {
            File.Create(filePath);
        }
        //转换成json格式的字符串
        string saveJsonStr = JsonMapper.ToJson(data);
        //写入文件
        StreamWriter sw = new StreamWriter(filePath);
        sw.Write(saveJsonStr);
        sw.Close();

    }
    /// <summary>
    /// 取数据
    /// </summary>
    /// <returns>数据类</returns>
    private Data LoadByJson() 
    {
        string filePath = Application.dataPath + "/StreamFile" + "/DataByJson.json";          
        if (File.Exists(filePath))
        {            
            StreamReader sr = new StreamReader(filePath);
            string jsonStr = sr.ReadToEnd();
            sr.Close();
            Data data = JsonMapper.ToObject<Data>(jsonStr);
            return data;
        }
        
        return null;
    }

    private Data LoadData()
    {
        Data data = LoadByJson();
        if(data ==null)
        {

            data = new Data();        
            for (int i = 0; i < MaxLevel; i++)
            {
                data.stars.Add(0);
                data.levels.Add(true);
            }
            
            for(int i=0;i<knowledgeMangers.Count;i++)
            {
                KnowledgeInfo info = new KnowledgeInfo();
                info.isGet = false;
                info.knowledgeMangerID = knowledgeMangers[i].GetID();                
                data.knowledgeInfos.Add(info);
            }

            print(data.knowledgeInfos.Count);
            return data;
        }
        return data;
        
    }
    
    /// <summary>
    /// 通过KnowledegManger的ID来判断是否得到
    /// </summary>
    /// <param name="ID">ID</param>
    /// <returns>是否得到</returns>
    public bool isGetKnowledegByID(int ID)
    {
        //if(knowledgeInfos.Count==0)
        //{
        //    DataBase.Instacne.UpdateKnowledgeMangers();
        //}
        for(int i=0;i<knowledgeInfos.Count;i++)
        {
            if(knowledgeInfos[i].knowledgeMangerID==ID)
            {
                return knowledgeInfos[i].isGet;
            }
        }
        Debug.LogWarning($"没有ID:{ID}");
        return false;
    }
    /// <summary>
    /// 改变ID对应isGet
    /// </summary>
    /// <param name="ID">ID</param>
    /// <param name="isGet">是否被得到</param>
    public void ChangeKnowledgeIsGet(int ID,bool isGet)
    {
        List<KnowledgeInfo> tempInfo = new List<KnowledgeInfo>();
        for (int i = 0; i < knowledgeInfos.Count; i++)
        {
            if (knowledgeInfos[i].knowledgeMangerID == ID)
            {
                KnowledgeInfo info = new KnowledgeInfo();
                info.knowledgeMangerID = knowledgeInfos[i].knowledgeMangerID;
                info.isGet = isGet;
                tempInfo.Add(info);
                //knowledgeInfos.Remove(knowledgeInfos[i]);
                //knowledgeInfos.Add(info);
                //knowledgeInfos.Sort()
                //print("bool" + isGet);
                //print("info Id" + info.knowledgeMangerID);
                //print("info bool" + info.isGet);
                
                
            }
            else
            {
                tempInfo.Add(knowledgeInfos[i]);
            }
           
        }
        knowledgeInfos = new List<KnowledgeInfo>(tempInfo);
        SaveByJson();
        //return;
        //Debug.LogError($"没有ID:{ID}");
    }
    public void UpdateKnowledgeInfos(List<KnowledgeInfo> knowledgeInfos)
    {
        this.knowledgeInfos = new List<KnowledgeInfo>(knowledgeInfos);
        SaveByJson();
    }
    private int GetAllStarCount()
    {
        int count = 0;
        for(int i=0;i<stars.Count;i++)
        {
            count += stars[i];
        }
        return count;
    }
}
