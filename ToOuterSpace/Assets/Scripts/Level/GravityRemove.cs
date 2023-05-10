using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 移除对应行星的引力
/// </summary>
public class GravityRemove : MonoBehaviour
{
    
    [SerializeField]
    [Header("对应行星")]
    Planet earth;
    /// <summary>
    /// 对应行星的引力失效范围
    /// </summary>
    [Header("对应行星的引力失效范围")]
    public int range = 2;

    
    /// <summary>
    /// 如果火箭与本卫星靠近达 rang 范围时则 移除 本卫星对应的 earth 产生的引力
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        if(Shuttle.shuttle==null)
        {
            return;
        }
        Vector2 offset = Shuttle.shuttle.transform.position - transform.position;
        if (offset.magnitude < range)
        {
            earth.mass = 0;
        }
        else
        {
            earth.mass = 12;
        }
    }
}
