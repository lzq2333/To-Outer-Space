using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 补给站
/// </summary>
public class Energy : MonoBehaviour
{
    [SerializeField]
    [Header("范围")]
    float range = 1;

    /// <summary>
    /// 是否是目标点
    /// </summary>
    [Header("是否是目标点")]
    public bool isTarget = false;

    SpriteRenderer spriteRenderer;

    [SerializeField]
    [Header("取走补给后的图")]
    Sprite noFuel;

    GameObject icon;
    // Start is called before the first frame update
    void Start()
    {
        icon = transform.GetChild(0).gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Shuttle.shuttle==null)
        {
            return;
        }
        Vector2 offset = transform.position - Shuttle.shuttle.transform.position;
        if (offset.sqrMagnitude < 0.5 && spriteRenderer.sprite != noFuel)
        {
            GetFuel();
        }
    }
    /// <summary>
    /// 火箭取走补给
    /// </summary>
    void GetFuel()
    {
        spriteRenderer.sprite = noFuel;
        spriteRenderer.color = Color.gray;
        Destroy(icon);
        if (isTarget)
        {
            End.EndGame(true);
        }
        else
        {
            Shuttle.shuttle.jet++;
        }
        
    }
}
