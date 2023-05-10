using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 星球
/// </summary>
public class Planet : MonoBehaviour
{
    [SerializeField]
    [Header("显示引力范围的物体")]
    GameObject rangeIdentifier;

    /// <summary>
    /// 星球半径
    /// </summary>
    [Header("星球半径")]
    public float radius = 1;

    /// <summary>
    /// 对物体产生力的效果的最大距离
    /// </summary>
    [Header("对物体产生力的效果的最大距离")]
    public float effect = 10;

    /// <summary>
    /// 质量比率（该值越大星球越重）
    /// </summary>
    [Header("质量比率（该值越大星球越重）")]
    public float mass = 1;

    

    // Start is called before the first frame update
    protected void Start()
    {
        Transform icon;
        if (rangeIdentifier != null)
        {
            icon = Instantiate(rangeIdentifier, transform).transform;
            icon.localScale = new Vector3(effect, effect, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 对原方法的多态，为了判断是否与路径点发生碰撞
    /// </summary>
    /// <param name="pos">物体位置</param>
    /// <param name="isPreview">是否绘制路径点</param>
    /// <param name="isCollider">存储是否发生碰撞</param>
    /// <returns>星球对物体的力</returns>
    public Vector2 GetForce(Vector2 pos, bool isPreview,out bool isCollider)
    {
        
        //与星球的位置的差值
        Vector2 delta = (Vector2)transform.position - pos;
        isCollider = false;
        if (delta.magnitude <= radius)
        {
            isCollider = true;
        }
        //撞击
        if (delta.magnitude <= radius && !isPreview)
        {
            //Debug.Log(gameObject.name);
            //失败
            Collide();
            return new Vector2(0, 0);
        }
        //影响范围
        else if (delta.magnitude >= effect) 
            return new Vector2(0, 0);

        float magnitude = mass / delta.sqrMagnitude;
        magnitude = Mathf.Min(magnitude, mass * 64);//防止距离过近，力过大
        return delta.normalized * magnitude;// * GameManger.Instance.Force;
    }
    /// <summary>
    /// 对物体产生的力
    /// </summary>
    /// <param name="pos">物体位置</param>
    /// <param name="isPreview">是否是绘制路径点</param>
    /// <returns>产生的力</returns>
    public Vector2 GetForce(Vector2 pos, bool isPreview)
    {
        
        //与星球的位置的差值
        Vector2 delta = (Vector2)transform.position - pos;
        
        //撞击
        if (delta.magnitude <= radius && !isPreview)
        {
            //Debug.Log(gameObject.name);
            //失败
            Collide();
            return new Vector2(0, 0);
        }
        //影响范围
        else if (delta.magnitude >= effect)
            return new Vector2(0, 0);

        float magnitude = mass / delta.sqrMagnitude;
        magnitude = Mathf.Min(magnitude, mass * 64);//防止距离过近，力过大
        return delta.normalized * magnitude;// * GameManger.Instance.Force;
    }

    /// <summary>
    /// 火箭碰撞了该星球
    /// </summary>
    public virtual void Collide()
    {
        End.EndGame(false);
    }
}
