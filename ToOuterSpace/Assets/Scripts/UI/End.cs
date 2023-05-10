using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// 结束
/// </summary>
public class End : MonoBehaviour
{
    public static End end;

    [SerializeField]
    Image[] stars;
    //[SerializeField]
    //Sprite star;
    [SerializeField]
    GameObject next;
    [SerializeField]
    GameObject current;
    [SerializeField]
    GameObject current_vectory;
    

    public int starCount = 0;
    public Image levelCnange;

    private int show = Animator.StringToHash("Show");
    /// <summary>
    /// 获取关卡数
    /// </summary>
    public int Level
    {
        get
        {
            string str =SceneManager.GetActiveScene().name;
            return int.Parse(str.Remove(0,5));
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        end = this;
        gameObject.SetActive(false);        
    }

    

    static Coroutine endCoroutine = null;

    /// <summary>
    /// 结束游戏，立即显示结束界面
    /// </summary>
    /// <param name="isVictory">是否胜利</param>
    public static void EndGame(bool isVictory)
    {
        //if (end.gameObject.activeSelf) return;
        //end.next.SetActive(isVictory);
        //end.current.SetActive(!isVictory);
        //end.gameObject.SetActive(true);
        //if (isVictory)
        //{
        //    for (int i = 0; i < 3; i++)
        //    {
        //        if (i <= Shuttle.shuttle.jet)
        //            end.stars[i].sprite = end.star;
        //    }
        //}
        //else
        //{
        //    Shuttle.shuttle.Explode();
        //}
        if (end.gameObject.activeSelf) return;
        end.gameObject.SetActive(true);
        end.StartCoroutine(end.EndGame(0.7f, isVictory));
        if(isVictory)
        {            
            GameManger.Instance.Upgrade (end.Level+1,Mathf.Clamp(end.GetStarCount(),1,3));
        }
    }

    /// <summary>
    /// 进入下一关
    /// </summary>
    public void Next()
    {
        int index = Level + 1;
        if (index <= GameManger.Instance.playerMaxLevel)
        {
            //SceneManager.LoadScene("Level" + index);
            StartCoroutine(DelayChangeScene(index));

        }
        else
        {
            SceneManager.LoadScene("Start");
        }
    }
    private IEnumerator DelayChangeScene(int index)
    {
        //EventCenter.Broadcast(EventDefine.SceneChangeStart);
        //SceneChange.Instance.OnLevelChangeStart();
        //SceneChangeByAnim.Instance.CloseView();
        yield return null;//new WaitForSeconds(1.2f);
        SceneManager.LoadScene("Level" + index);
        //async.completed += ShowNewScene;//()=> { EventCenter.Broadcast(EventDefine.SceneChangeStart); };
                                        //.completed+=
        
    }
   

    /// <summary>
    /// 重玩当前关卡
    /// </summary>
    public void Current()
    {
        SceneManager.LoadScene("Level" + Level);
    }
    /// <summary>
    /// 等待一定时间后显示结束画面
    /// </summary>
    /// <param name="t">等待的时间</param>
    /// <param name="isVictory">是否胜利</param>
    /// <returns></returns>
    IEnumerator EndGame(float t ,bool isVictory)
    {
        if (!isVictory)
        {
            Shuttle.shuttle.Explode();
        }
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(t);
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(true);
        end.next.SetActive(isVictory);
        end.current_vectory.SetActive(isVictory);
        end.current.SetActive(!isVictory);
        if (isVictory)
        {
            StartCoroutine(ShowStar());
            
        }
    }
    private int GetStarCount()
    {
        if (Shuttle.shuttle.jet * 1.0f / Shuttle.shuttle.totalJet >= 0.5f)
        {
            return 3;
        }
        if (Shuttle.shuttle.jet * 1.0f / Shuttle.shuttle.totalJet >= 0.25f)
        {
            return 2;
        }
        return 1;
    }
    private IEnumerator ShowStar()
    {
        for(int i=0;i<GetStarCount();i++)
        {
            stars[i].color = Color.white;
            stars[i].GetComponent<Animator>().SetTrigger(show);
            stars[i].transform.parent.GetChild(1).gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
        }
    }
}
