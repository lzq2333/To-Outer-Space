using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ֪ʶ�������
/// </summary>
public class TheKnowledge : MonoBehaviour
{
    [SerializeField]
    [Header("��Χ")]
    private float range = 1;

    
    private KnowledgeManger knowledgeManger;

    /// <summary>
    /// ��ӦID
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
    /// �ж��Ƿ�����ײ
    /// </summary>
    /// <returns>�Ƿ�����ײ</returns>
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
