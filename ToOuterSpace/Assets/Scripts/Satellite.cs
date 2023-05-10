using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 星球的子类，卫星类
/// </summary>
public class Satellite : Planet
{
    /// <summary>
    /// 该卫星对应的行星
    /// </summary>
    [Header("该卫星对应的父级星球")]    
    public Transform center;
    /// <summary>
    /// 公转速度
    /// </summary>
    [Header("公转速度")]
    public float angularSpeed = 1;


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
    /// 卫星公转
    /// </summary>
    private void FixedUpdate()
    {
        if(center==null)
        { 
            return;
        }
        Vector3 offset = transform.position - center.position;
        transform.position = center.position +
           Quaternion.Euler(0, 0, angularSpeed * Shuttle.interval * Shuttle.timeScale * Shuttle.shuttle.speedRate) * offset;
        
    }
}
