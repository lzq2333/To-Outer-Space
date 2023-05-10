using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    /// <summary>
    /// 存储星星数
    /// </summary>
    public List<int> stars = new List<int>();

    /// <summary>
    /// 存储当前语言
    /// </summary>
    public string language="cns";

    /// <summary>
    /// 通过的关卡
    /// </summary>
    public int level = 1;
    /// <summary>
    /// 存储是不是第一次玩
    /// </summary>
    //public Dictionary<int, bool> levels = new Dictionary<int, bool>();
    public List<bool> levels=new List<bool>();
    //public Data(int maxLevel)
    //{
    //    for(int i=0;i<maxLevel;i++)
    //    {
    //        stars.Add(0);
    //        levels.Add(true);
    //    }
    //}
    /// <summary>
    /// 存储KnowledgeManger信息
    /// </summary>
    public List<KnowledgeInfo> knowledgeInfos = new List<KnowledgeInfo>();
}
