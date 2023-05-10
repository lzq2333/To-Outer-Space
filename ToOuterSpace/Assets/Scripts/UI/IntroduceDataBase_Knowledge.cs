using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class IntroduceDataBase_Knowledge : MonoBehaviour
{
    [SerializeField]
    private VictoryLanding victoryLanding;
    [SerializeField]
    private Button btn_close;
    private void Awake()
    {
        EventCenter.AddListener(EventDefine.ColliderKnowledge, ColliderKnowledge);        
        btn_close.onClick.AddListener(Close);
        transform.localScale = Vector3.zero;
    }
    private void OnDestroy()
    {        
        EventCenter.RemoveListener(EventDefine.ColliderKnowledge, ColliderKnowledge);
    }
    private void ColliderKnowledge()
    {
        if (!GameManger.Instance.isGetKnowledegByID(1))
        {
            Time.timeScale = 0;
            transform.DOScale(Vector3.one, 0.6f).SetUpdate(true);
        }
        else
        {
            victoryLanding.ChangeVictoryTime(0.01f);
        }
    }
    private void Close()
    {

        transform.DOScale(Vector3.zero, 0.6f).SetUpdate(true).
                    OnComplete(() => { Time.timeScale = 1; victoryLanding.ChangeVictoryTime(0.01f); });

    }
}
