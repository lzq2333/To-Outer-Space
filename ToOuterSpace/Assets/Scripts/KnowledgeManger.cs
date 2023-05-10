using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct KnowledgeInfo
{
    /// <summary>
    /// 是否被得到
    /// </summary>
    public bool isGet;
    /// <summary>
    /// 对应KnowledgeManger的ID
    /// </summary>
    public int knowledgeMangerID;

}
/// <summary>
/// 知识管理类
/// </summary>
[System.Serializable]
public class KnowledgeManger : MonoBehaviour
{
    
    /// <summary>
    /// 唯一ID
    /// </summary>
    [SerializeField]
    [Header("唯一ID")]
    private int myID;

    public bool isGet = false;
    
    void Start()
    {
        isGet = GameManger.Instance.isGetKnowledegByID(myID); 
    }
    
    public int GetID()
    {
        return myID; 
    }
    public KnowledgeManger GetKnowledgeMangerByID(int ID)
    {
        if(ID==myID)
        {
            return this;
        }
        else
        {
            return null;
        }
    }
}
