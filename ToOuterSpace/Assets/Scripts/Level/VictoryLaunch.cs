using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 飞船大于某个范围胜利
/// </summary>
public class VictoryLaunch : MonoBehaviour
{
    Image image;

    [SerializeField]
    [Header("飞船要大于的范围")]
    float range = 1;
    /// <summary>
    /// 计时器
    /// </summary>
    float victoryTimer = 0;
    [SerializeField]
    [Header("飞船要停留的时间")]
    float time = 3;

    /// <summary>
    /// 游戏胜利控制器
    /// </summary>
    private VictoryController victoryController;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        victoryController = transform.parent.parent?.GetComponent<VictoryController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Setting.setting.gameObject.activeSelf)
        {
            return;
        }
        if(victoryController!=null)
        {
            ControlByParent();
        }
        else
        {
            ControlBySelf();
        }
    }
    /// <summary>
    /// 由VectoryController来进行控制
    /// </summary>    
    private void ControlByParent()
    {
        if (Shuttle.shuttle == null)
        {
            return;
        }
        Vector2 offset = Shuttle.shuttle.transform.position - transform.position;
        if (offset.magnitude > range)
        {
            victoryController.isOut = true;
        }
        else
        {
            victoryController.isOut = false;
        }
    }
    /// <summary>
    /// 靠自己控制
    /// </summary>
    private void ControlBySelf()
    {
        if (Shuttle.shuttle == null)
        {
            return;
        }
        image.fillAmount = victoryTimer / time;
        Shuttle.shuttle.Winprogress = image.fillAmount;
        Vector2 offset = Shuttle.shuttle.transform.position - transform.position;
        if (offset.magnitude > range)
        {
            victoryTimer += Time.deltaTime * Shuttle.timeScale * Shuttle.shuttle.speedRate;
            if (victoryTimer > time)
            {
                image.fillAmount = 0;
                End.EndGame(true);
            }
        }
        else
        {
            victoryTimer = 0;
        }
    }
}
