using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 绘画路径
/// </summary>
public class PathPreviewer : MonoBehaviour
{
    public static PathPreviewer viewer;
    
    
    [SerializeField]
    [Header("路径点")]
    GameObject point;
    /// <summary>
    /// 路径点的个数
    /// </summary>
    int previewCount = 20;
    /// <summary>
    /// 存储点的位置
    /// </summary>
    Transform[] points;

    private void Awake()
    {
        viewer = this;
        points = new Transform[previewCount];
        for(int i = 0; i < previewCount; i++)
        {
            points[i] = GameObject.Instantiate(point, transform.position, Quaternion.identity, transform).transform;
            points[i].position += new Vector3(0, 0, -5);
            //points[i].gameObject.SetActive(false);
        }
        
    }

    

    // Update is called once per frame
    void Update()
    {
        if(Shuttle.timeScale==0 || (Setting.setting && Setting.setting.gameObject.activeSelf))
        {
            return;
        }
        //Move();
        Move_New();
    }
    
    
    /// <summary>
    /// 路径点移动
    /// </summary>
    private void Move_New()
    {
        if (Shuttle.shuttle.canCtrl && Shuttle.shuttle.jet > 0 )
        {
            Vector2 delta = Shuttle.shuttle.delta;
            Preview(delta.normalized * Shuttle.acceleration);
        }
        else
        {
            Preview(new Vector2(0, 0));
        }
    }
    
    /// <summary>
    /// 绘制路径点的位置
    /// </summary>
    /// <param name="acceleration"></param>
    public void Preview(Vector2 acceleration)
    {
        Vector2 velocity = Shuttle.shuttle.velocity + acceleration;
        //存储火箭的位置
        Vector3 position = Shuttle.shuttle.transform.position;
        position.z = -5;
        for (int i = 0; i < previewCount; i++)
        {
            points[i].gameObject.SetActive(true);

            Vector2 velocity_temp = velocity;
            //判断是否跳出循环
            bool tag = false;
            for (int j = 0; j < 15; j++)
            {                
                
                //重力影响
                foreach (Planet planet in StarManager.Planets)
                {
                    bool isCollider = false;
                    velocity += planet.GetForce(position, true,out isCollider) * Shuttle.interval;
                    if(isCollider)
                    {
                        points[i].gameObject.SetActive(false);
                        velocity = velocity_temp;
                        tag = true;
                        break;
                    }
                }
                if(tag)
                {
                    break;
                }
                position += (Vector3)velocity * Shuttle.interval;
            }
            if(!tag)
            {
                points[i].position = position;
            }
            
        }
    }


}
