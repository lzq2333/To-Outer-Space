using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 开始界面
/// </summary>
public class StartGame : MonoBehaviour
{
    /// <summary>
    /// 跳转选择关卡界面
    /// </summary>
    public void LevelSelect()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect");
    }
    /// <summary>
    /// 退出游戏
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

}
