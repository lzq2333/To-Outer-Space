using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 知识物体管理
/// </summary>
public class TheKnowledge : MonoBehaviour
{
    [SerializeField]
    [Header("范围")]
    private float range = 1;

    
    private KnowledgeManger knowledgeManger;

    /// <summary>
    /// 对应ID
    /// </summary>
    [SerializeField]
    private int myID;
    private void Awake()
    {
        for(int i=0;i<GameManger.Instance.knowledgeMangers.Count;i++)
        {
            if(GameManger.Instance.knowledgeMangers[i].GetID()==myID)
            {
                knowledgeManger = GameManger.Instance.
                    knowledgeMangers[i].GetKnowledgeMangerByID(myID);
            }
        }
        
    }
    private void Update()
    {
        if(Collide())
        {
            EventCenter.Broadcast(EventDefine.ColliderKnowledge);
            GameManger.Instance.ChangeKnowledgeIsGet(myID,true);
            //print(GameManger.Instance.isGetKnowledegByID(myID));
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// 判断是否发生碰撞
    /// </summary>
    /// <returns>是否发生碰撞</returns>
    public bool Collide()
    {
        if (Shuttle.shuttle != null)
        {
            float distance = Vector2.Distance(transform.position,
                Shuttle.shuttle.gameObject.transform.position);
            if (distance < range)
            {
                return true;
            }
        }
        return false;
    }
}
