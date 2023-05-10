using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySTar : MonoBehaviour
{
    /// <summary>
    /// 边距
    /// </summary>
    [SerializeField]
    private static readonly float dis = 30;

    private  float LeftRange = 0 + dis;
    private  float RightRange = Screen.width- dis;
    private  float TopRange = Screen.height  - dis;
    private  float BottomRange = 0 + dis;
    /// <summary>
    /// 速度
    /// </summary>
    [SerializeField]
    private float speed = 5;
    /// <summary>
    /// 方向
    /// </summary>
    public Vector2 dir;

    private bool canFly = true;
    private GameObject trail;
    private void Awake()
    {
        trail = transform.Find("Trail").gameObject;
    }
    void Start()
    {
        dir = new Vector2(Random.Range(0f, 1), Random.Range(0f, 1));
        dir.Normalize();

    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        if(!canFly)
        {
            trail.SetActive(false);
            return;
        }
        else
        {
            trail.SetActive(true);
        }
        transform.Translate(dir * speed * Time.deltaTime);
        Vector2 currentPos = Camera.main.WorldToScreenPoint(transform.position);
        if (currentPos.y > TopRange && dir.y>0)
        {
            dir.y *= -1;
        }
        if (currentPos.y < BottomRange && dir.y<0)
        {
            dir.y *= -1;
        }
        if (currentPos.x > RightRange&&dir.x>0)
        {
            dir.x *= -1;
        }
        if ( currentPos.x < LeftRange&&dir.x<0)
        {
            dir.x *= -1;
        }
    }
    private void OnMouseDown()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lefpPos = mousePos - (Vector2)transform.position;
        dir -= lefpPos;
        dir.Normalize();
    }
    private void OnMouseDrag()
    {
        canFly = false;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
    }
    private void OnMouseUp()
    {
        canFly = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("FlyStar"))
        {
            Vector2 collidePos = collision.contacts[0].point;
            Vector2 lefpPos = collidePos - (Vector2)transform.position;
            lefpPos.Normalize();
            dir -= lefpPos;
            dir.Normalize();
        }
    }
}
