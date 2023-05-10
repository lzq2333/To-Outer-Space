using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 关卡选择
/// </summary>
public class LevelSelect : MonoBehaviour
{
    [Header("关卡按键模板")]
    public GameObject buttonTemplate;

    [SerializeField]
    private Text txt_totalStarCount;

    [SerializeField]
    [Header("关卡父级面板")]
    private GameObject levelPanel;
    private List<Transform> levelPanels=new List<Transform>();
    [SerializeField]
    private Button btn_last;
    [SerializeField]
    private Button btn_next;

    
    /// <summary>
    /// 当前页面数
    /// </summary>
    private int currentPage = 1;
    //public float width = 180;
    //public float height = 120;
    
    /// <summary>
    /// 产生关卡按键
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
                
        //enable应该是存储过到第几个关了
        int enable = GameManger.Instance.Level;

        levelPanels.Add(levelPanel.transform);

        btn_last.onClick.AddListener(OnLastButtonClick);
        btn_next.onClick.AddListener(OnNextButtonClick);

        btn_last.gameObject.SetActive(GameManger.Instance.MaxLevel > 8);
        btn_next.gameObject.SetActive(GameManger.Instance.MaxLevel > 8);
       
        for (int i = 0; i < GameManger.Instance.MaxLevel / 8 - (GameManger.Instance.MaxLevel % 8 == 0 ? 1 : 0); i++)
        {
            GameObject levelPanel = Instantiate(this.levelPanel,transform);
            levelPanels.Add(levelPanel.transform);
                        
        }
        
        for (int i = 0; i < GameManger.Instance.MaxLevel; i++)
        {
            //GameObject button = Instantiate(buttonTemplate, transform.position +
            //    new Vector3((i % 4 - 1.5f) * width, -(i / 4 - 0.5f) * height, 0),
            //    Quaternion.identity, transform);
            GameObject button = Instantiate(buttonTemplate, levelPanels[(i+1) / 8 - ((i+1) < 8 ? 0 :((i + 1) % 8 == 0 ? 1 : 0) )]);
            button.GetComponent<LevelButton>().level = i + 1;  
            button.name = "" + (i + 1);

            //将已经过了的关和正在过得关的LevelButton 类的 isEnable 设为 true ，将不可以玩的关卡设为 false
            //      -----应该是想要做要一关一关过的功能（未完成）------
            //print(button.GetComponent<LevelButton>());
            button.GetComponent<LevelButton>().SetActive((i+1) <= enable);
            //print(i + 1);
            txt_totalStarCount.text = "X " + GameManger.Instance.totalStarCount;
        }
        
        for(int i=1;i<levelPanels.Count;i++)
        {
            
            levelPanels[i].gameObject.SetActive(false);
        }
    }
    private void OnNextButtonClick()
    {
        if(currentPage>=levelPanels.Count)
        {
            return;
        }
        levelPanels[currentPage-1].gameObject.SetActive(false);
        currentPage++;
        levelPanels[currentPage-1].gameObject.SetActive(true);
    }
    private void OnLastButtonClick()
    {
        if (currentPage <= 1)
        {
            return;
        }
        levelPanels[currentPage-1].gameObject.SetActive(false);
        currentPage--;
        levelPanels[currentPage-1].gameObject.SetActive(true);
    }
}
