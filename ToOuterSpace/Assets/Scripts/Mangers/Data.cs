using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    /// <summary>
    /// �洢������
    /// </summary>
    public List<int> stars = new List<int>();

    /// <summary>
    /// �洢��ǰ����
    /// </summary>
    public string language="cns";

    /// <summary>
    /// ͨ���Ĺؿ�
    /// </summary>
    public int level = 1;
    /// <summary>
    /// �洢�ǲ��ǵ�һ����
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
    /// �洢KnowledgeManger��Ϣ
    /// </summary>
    public List<KnowledgeInfo> knowledgeInfos = new List<KnowledgeInfo>();
}
