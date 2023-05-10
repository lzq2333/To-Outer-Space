using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITip : MonoBehaviour
{
    /// <summary>
    /// 当前页面数下标
    /// </summary>
    private int currentIndex;
    /// <summary>
    /// 总页面数下标
    /// </summary>
    public int allIndex;

    

    public GameObject close;
    
    private void Awake()
    {
        
        currentIndex = 0;
        //contents = transform.Find("Img_ContentPanel/Togs").GetComponentsInChildren<Toggle>();
        

        
        if (GameManger.Instance.sceneDic[End.end.Level])
        {
            
            close.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (currentIndex == allIndex)
        {
            close.SetActive(true);
        }
    }
    //public void OnLastButtonClick()
    //{
    //    currentIndex = Mathf.Clamp(currentIndex - 1, 0, allIndex);
        
    //    //contents[currentIndex].isOn = true;
    //}
    //public void OnNextButtonClick()
    //{
    //    currentIndex = Mathf.Clamp(currentIndex + 1, 0, allIndex);
    //    contents[currentIndex].isOn = true;
        
    //}
    public void Close()
    {        
        gameObject.SetActive(false);
        Time.timeScale = 1;
        int tag=0;
        int.TryParse(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Remove(0, 5),out tag);
        if(tag !=0)
        {
            //显示最佳路径点
            if (tag ==1 ||tag==2||tag==3)
                BestPathIntroduce.Instance?.Show();
            else
            {
                //显示视频
                if (Once.Instance != null && UIIntroduce.GetUIIntroduce().videoTips[tag - 1].videoClip[0] != null)
                {
                    Once.Instance.OncePlay();
                }
            }
        }
    }
}
