using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct KnowledgeInfo
{
    /// <summary>
    /// �Ƿ񱻵õ�
    /// </summary>
    public bool isGet;
    /// <summary>
    /// ��ӦKnowledgeManger��ID
    /// </summary>
    public int knowledgeMangerID;

}
/// <summary>
/// ֪ʶ������
/// </summary>
[System.Serializable]
public class KnowledgeManger : MonoBehaviour
{
    
    /// <summary>
    /// ΨһID
    /// </summary>
    [SerializeField]
    [Header("ΨһID")]
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
