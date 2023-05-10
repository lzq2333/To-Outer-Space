using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 飞船小于某个范围游戏胜利
/// </summary>
public class VictoryLanding : MonoBehaviour
{
    protected Image image;

    [SerializeField]
    [Header("飞船要小于的范围")]
    protected float range = 1;
    /// <summary>
    /// 计时器
    /// </summary>
    protected float victoryTimer = 0;
    [SerializeField]
    [Header("停留时间")]
    protected float time = 3;

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
        if(Setting.setting && Setting.setting.gameObject.activeSelf)
        {
            return;
        }
        if(victoryController !=null)
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
        //print(((Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position)).magnitude);
        if (offset.magnitude < range)
        {
            victoryController.isIn = true;
        }
        else
        {
            victoryController.isIn = false;
        }
    }
    /// <summary>
    /// 靠自己控制
    /// </summary>
    protected virtual void ControlBySelf()
    {
        if (Shuttle.shuttle == null)
        {
            return;
        }
        image.fillAmount = victoryTimer / time;
        Shuttle.shuttle.Winprogress = image.fillAmount;
        Vector2 offset = Shuttle.shuttle.transform.position - transform.position;
        if (offset.magnitude < range)
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
    public void ChangeVictoryTime(float victoryTime)
    {
        time = victoryTime;
    }
}
