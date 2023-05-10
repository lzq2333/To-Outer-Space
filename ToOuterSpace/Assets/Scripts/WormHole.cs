using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 虫洞
/// </summary>
public class WormHole : Planet
{


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
    }
    /// <summary>
    /// 如果碰撞则出现开始本关游戏
    /// </summary>
    public override void Collide()
    {
        string str = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        int level = int.Parse(str[5] + "");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level" + level);
    }
}
