using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 关卡按键
/// </summary>
public class LevelButton : MonoBehaviour
{
    ///// <summary>
    ///// 该关卡的图
    ///// </summary>   
    //[Header("该关卡的图")]
    //Sprite img;

    private Image btnImg;

    /// <summary>
    /// 锁
    /// </summary>
    [SerializeField]
    [Header("锁")]
    GameObject img_lock;
    

    /// <summary>
    /// 当前关卡是否可以玩
    /// </summary>
    [Header("当前关卡是否可以玩")]
    public bool isEnable;
    
    /// <summary>
    /// 当前关卡等级
    /// </summary>
    [Header("当前关卡等级")]
    public int level;
    Text text;
    /// <summary>
    /// 星星
    /// </summary>
    [SerializeField]
    private GameObject stars;

    /// <summary>
    /// 星星数量
    /// </summary>
    [HideInInspector]
    public int starNum=0;

    [SerializeField]
    [Header("没有得到的星星的颜色")]
    private Color disStarColor;
    private void Awake()
    {
        btnImg = GetComponent<Image>();
        text = transform.GetChild(0).GetComponent<Text>();
        
        
    }

    // Start is called before the first frame update
    void Start()
    {
        text.text = gameObject.name;
        
        btnImg.sprite = UIIntroduce.GetUIIntroduce().levelButtons[level - 1];
    }

    // Update is called once per frame
    void Update()
    {
        //if (isEnable)
        //{
        //    GetComponent<Image>().sprite = enable;
        //}
        //else
        //{
        //    GetComponent<Image>().sprite = disable;
        //}
    }
    /// <summary>
    /// 关卡按键点击加载对应游戏场景
    /// </summary>
    public void Click()
    {
        if (isEnable)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level" + gameObject.name);
            //EventCenter.Broadcast(EventDefine.SceneChange, int.Parse(gameObject.name));
        }
            
    }
    /// <summary>
    ///  激活/关闭 当前关卡
    /// </summary>
    /// <param name="b">激活/关闭</param>
    public void SetActive(bool b)
    {
        
        isEnable = b;
        //print(GameManger.Instance.stars.Count);
        
        starNum = GameManger.Instance.stars[int.Parse(gameObject.name) - 1];
        if (isEnable)
        {
            if(starNum==0)
            {
                stars.SetActive(false);
            }
            else
            {
                for(int i=0;i<3;i++)
                {
                    if(i<starNum)
                    {
                        stars.transform.GetChild(i).GetComponent<Image>().color = Color.white;
                        continue;
                    }
                    stars.transform.GetChild(i).GetComponent<Image>().color = disStarColor;
                }
            }
            btnImg.color = Color.white;
            img_lock.SetActive(false);
        }
        else
        {
            stars.SetActive(false);
            btnImg.color = Color.grey;
            img_lock.SetActive(true);
        }
    }
}
