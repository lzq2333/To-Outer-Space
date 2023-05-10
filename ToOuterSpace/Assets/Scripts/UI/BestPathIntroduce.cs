using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestPathIntroduce : MonoBehaviour
{
    private static BestPathIntroduce _instance;
    public static BestPathIntroduce Instance { get => _instance; private set => _instance = value; }
    /// <summary>
    /// �Ի�������
    /// </summary>
    public int talkCount;
    /// <summary>
    /// ��ǰ�Ի���
    /// </summary>
    public int currentTalk;
    /// <summary>
    /// �Ի���
    /// </summary>
    [SerializeField]
    private GameObject talkPanel;
    
    /// <summary>
    /// �Ի�������
    /// </summary>
    public List<string> talkScentenses = new List<string>();
    
    [HideInInspector]
    public List<TalkPanel> talkPanels = new List<TalkPanel>();
    /// <summary>
    /// �Ի���λ��
    /// </summary>
    [SerializeField]
    private Vector2 talkPanelPosition;
    
    public List<Vector2> hintPositions=new List<Vector2>();
    
    public List<Quaternion> hintRotations = new List<Quaternion>();

    private void Awake()
    {
        Instance = this;
       
        for (int i=0;i<talkCount;i++)
        {
            GameObject talkPanel = Instantiate(this.talkPanel,transform,false);
            talkPanel.transform.localPosition = talkPanelPosition;
            talkPanel.SetActive(false);
            talkPanels.Add(talkPanel.GetComponent<TalkPanel>());
        }

        if (3 == int.Parse(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Remove(0, 5)))
            transform.SetAsFirstSibling();
        gameObject.SetActive(false);
        
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
        talkPanels[0].Show(talkScentenses[0]);
        Time.timeScale = 0;
    }
    
    public void Close()
    {
        Time.timeScale = 1;
        //Destroy(gameObject);
        int tag = 0;
        int.TryParse(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Remove(0, 5), out tag);
        if (Once.Instance != null && UIIntroduce.GetUIIntroduce().videoTips[tag - 1].videoClip[0] != null)
        {
            
            Once.Instance.OncePlay();
        }
        Destroy(gameObject);
        //for(int i=transform.childCount-1;i>0;i--)
        //{
        //    Destroy(transform.GetChild(i).gameObject);
        //}
    }
}
