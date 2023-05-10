using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TalkPanel : MonoBehaviour
{
    Tweener tweener_dotext;
    /// <summary>
    /// 可以下一个标识
    /// </summary>
    public GameObject canNextTag;

    public GameObject hintPre;
    private GameObject hint;
    /// <summary>
    /// 显示下一个文本框的时间
    /// </summary>
    public float showNextTextTime = 2f;

    private Text txt_talk;
    private Button btn_showNext;
    private Button btn_showAllText;
    private void Awake()
    {
        txt_talk = transform.Find("Text").GetComponent<Text>();
        btn_showNext= transform.Find("Click").GetComponent<Button>();
        btn_showAllText = transform.Find("ShowAllText").GetComponent<Button>();
        btn_showAllText.onClick.AddListener(()=>{ tweener_dotext?.Complete(); });
        canNextTag.SetActive(false);
        btn_showNext.gameObject.SetActive(false);
        hint = Instantiate(hintPre, transform);
        gameObject.SetActive(false);
        
    }
    public void Show(string talkText)
    {
        gameObject.SetActive(true);
        BestPathIntroduce.Instance.currentTalk++;
        hint.GetComponent<RectTransform>().anchoredPosition = BestPathIntroduce.Instance.hintPositions[BestPathIntroduce.Instance.currentTalk - 1];
        hint.transform.rotation= BestPathIntroduce.Instance.hintRotations[BestPathIntroduce.Instance.currentTalk - 1];
        tweener_dotext = txt_talk.DOText(talkText, showNextTextTime).SetUpdate(true).
           OnComplete(() =>
           {
               
               canNextTag?.SetActive(true);
               btn_showNext?.gameObject.SetActive(true);
               if (BestPathIntroduce.Instance.currentTalk == BestPathIntroduce.Instance.talkCount)
               {

                   //改成关闭
                   btn_showNext.onClick.AddListener(BestPathIntroduce.Instance.Close);
               }
               else
               {
                   btn_showNext?.onClick.AddListener(() =>
                            {
                                Destroy(gameObject);
                                if (canNextTag)
                                {
                                    Destroy(canNextTag);
                                }
                                BestPathIntroduce.Instance.talkPanels[BestPathIntroduce.Instance.currentTalk].
                                Show(BestPathIntroduce.Instance.talkScentenses[BestPathIntroduce.Instance.currentTalk]);
                            });
               }

           });
    }
}
