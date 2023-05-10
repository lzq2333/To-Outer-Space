using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 退出
/// </summary>
public class Quit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 退出游戏，回到开始界面
    /// </summary>
    public void QuitGame()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
    }
    public void ReturnLevelSelect()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect");
        
    }
}
