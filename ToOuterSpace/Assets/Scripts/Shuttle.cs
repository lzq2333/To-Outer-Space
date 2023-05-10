using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 用于存储火箭信息和操作火箭
/// </summary>

public class Shuttle : MonoBehaviour
{
    [SerializeField]
    [Header("爆炸")]
    GameObject explosive;
    [SerializeField]
    [Header("喷气")]
    GameObject thrust;
    
    [SerializeField]
    [Header("烟的预制体")]
    private GameObject canCntrolSmokePre;
    private GameObject canCntrolSmoke;
    public static Shuttle shuttle;
    [Header("喷气次数")]
    public int jet = 3;
    /// <summary>
    /// 总喷气次数
    /// </summary>
    public int totalJet;
    /// <summary>
    /// 类似Time.deltaTime,对运动的频率进行约束
    /// </summary>
    public const float interval = 0.02f;
    /// <summary>
    /// 自定义比率，可看成火箭功率
    /// </summary>
    public const float acceleration = 0.5f;
    /// <summary>
    /// 速度比率，在改变位置时慢，平时快
    /// </summary>
    public static float timeScale = 1;

    /// <summary>
    /// 速度比率，用于按钮控制速度
    /// </summary>
    public float speedRate = 1;

    public Vector2 velocity = new Vector2(0, 0);

    /// <summary>
    /// 记录是否可以控制喷气
    /// </summary>
    [HideInInspector]
    public bool canCtrl = false;
    /// <summary>
    /// 准备离开设置，取消了设置后留的时间间隔，让火箭燃料不会因为点击了取消设置而减少
    /// </summary>
    float setTimer = 0;

    /// <summary>
    /// 火箭燃料用完后显示失败的等待时间
    /// </summary>
    public float waitTime = 4;
    public float waitTimer = 0;

    ///// <summary>
    ///// 喷气位置
    ///// </summary>
    //[Header("喷气位置")]
    //public Transform Hintpos;
    /// <summary>
    /// 喷气方向指示
    /// </summary>
    //[Header("喷气方向指示")]
    //public GameObject hint;
    /// <summary>
    /// 箭头长度比率
    /// </summary>
    [Header("箭头长度比率")]
    public float lengthRate = 0.22f;

    private bool isInVisibel = false;

    /// <summary>
    /// 消失视野的等待时间
    /// </summary>
    [Header("消失视野的等待时间")]
    public float inVisibleTime = 5;
    public float inVisibleTimer = 0;

    /// <summary>
    /// 鼠标与飞船的距离
    /// </summary>
    [Header("飞船与鼠标的距离")]
    public float ctrlDistance;

    public float MaxCtrlDiatance = 150f;
    /// <summary>
    /// 边界距离
    /// </summary>
    public float engleDistance = 5;

    public Vector2 delta;
    /// <summary>
    /// 完成进度百分比
    /// </summary>
    public float Winprogress = 0;
    /// <summary>
    /// 是否两秒后输掉
    /// </summary>
    private bool loseInTwoSeconds = false;

    /// <summary>
    /// 缩略图摄像机
    /// </summary>
    public Camera miniCamera;

    [SerializeField]
    private Texture2D ctrlTex;
    [SerializeField]
    private Texture2D commonTex;

    private void Awake()
    {        
        shuttle = this;        
    }

    // Start is called before the first frame update
    void Start()
    {
        totalJet = jet;
    }

    private void Update()
    {
        if (miniCamera != null)
        {
            Vector2 myPos = Camera.main.WorldToScreenPoint(transform.position);
            float rate = miniCamera.orthographicSize / Camera.main.orthographicSize;
            Vector3 miniViewUp = Camera.main.WorldToScreenPoint(miniCamera.gameObject.transform.position) +
                new Vector3(Screen.width / 2 * rate, Screen.height / 2 * rate, 0);
            Vector3 miniViewDown = Camera.main.WorldToScreenPoint(miniCamera.gameObject.transform.position) -
                new Vector3(Screen.width / 2 * rate, Screen.height / 2 * rate);

            if (myPos.x > miniViewUp.x || myPos.x < miniViewDown.x || myPos.y > miniViewUp.y || myPos.y < miniViewDown.y)
            {
                Camera.main.GetComponent<Follow>().enabled = false;
            }
            else
            {
                Camera.main.GetComponent<Follow>().enabled = true;
            }
        }

        if (Setting.setting && Setting.setting.gameObject.activeSelf)
        {
            setTimer = 0.1f;
            return;
        }
        if(jet==0)
        {
            OnJetEqualZero();
            if (loseInTwoSeconds)
            {
                waitTimer += Time.deltaTime * Time.timeScale * Shuttle.shuttle.speedRate;
            }
            else
            {
                waitTimer = 0;
            }
            
            if (waitTimer >= waitTime)
            {
                End.EndGame(false);
            }
            
        }
        
        setTimer -= Time.deltaTime * Shuttle.shuttle.speedRate;

        //喷射
        //ChangeVelocity();
        ChangeVelocity_New();

        OverByLeaveView();
    }
    /// <summary>
    /// 是否两秒后输掉
    /// </summary>
    private void OnJetEqualZero()
    {
        if (jet == 0 && Winprogress == 0)
        {
            loseInTwoSeconds = true;
        }
        else
        {
            loseInTwoSeconds = false;
        }
    }
    private void FixedUpdate()
    {
        if (Setting.setting && Setting.setting.gameObject.activeSelf)
        {
             return;
        }
        //引力影响
        foreach (Planet planet in StarManager.Planets)
        {
            velocity += planet.GetForce(transform.position, false) * interval * timeScale * Shuttle.shuttle.speedRate;
        }

        transform.position += (Vector3)velocity * interval * timeScale * Shuttle.shuttle.speedRate;
    }

    
    
    /// <summary>
    /// 爆炸
    /// </summary>
    public void Explode()
    {
        Instantiate(explosive, transform.position, Quaternion.identity, null);
        Destroy(gameObject);
    }
    
    /// <summary>
    /// 更新的喷气
    /// </summary>
    void ChangeVelocity_New()
    {

        
        //print(cancel.parent.parent.localScale.x);
        //print(cancel.position/cancel.parent.parent.localScale.x);
        Quaternion quaternion;
        
        if (Input.GetMouseButtonDown(0) && jet > 0 && !EventSystem.current.IsPointerOverGameObject())
        {
            canCtrl = true;
            
        }
        
        
        if(canCtrl)
        {
            Cursor.SetCursor(ctrlTex, new Vector2(0.5f, 0.5f), CursorMode.Auto);
            if(canCntrolSmoke==null)
            {
                canCntrolSmoke = Instantiate(canCntrolSmokePre, transform);
            }
            timeScale = 0.1f;
            //hint.SetActive(true);
            //hint.transform.localScale = new Vector3(Mathf.Clamp(-delta.magnitude * lengthRate,-0.45f,-0.1f), -0.2f, 1);
            quaternion = Quaternion.Euler(0, 0, Mathf.Atan2(delta.y, delta.x) * 180 / 3.14f);
        }
        else
        {
            Cursor.SetCursor(commonTex, new Vector2(0.05f, 0.1f), CursorMode.Auto);
            if (canCntrolSmoke!=null)
            {
                Destroy(canCntrolSmoke);
            }
            timeScale = 1f;
            //hint.SetActive(false);
            quaternion = Quaternion.Euler(0, 0, Mathf.Atan2(velocity.y, velocity.x) * 180 / 3.14f);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, 5 * Time.unscaledDeltaTime);

        if(canCtrl && Input.GetMouseButtonUp(0))
        {

        }

        if (canCtrl && Input.GetMouseButtonUp(0) && (
            ctrlDistance < MaxCtrlDiatance-engleDistance) && setTimer < 0)
        {
            if (jet > 0)
            {
                jet--;
                velocity += delta.normalized * acceleration;
                GetComponent<AudioSource>().Play();
                Instantiate(thrust, transform.position, transform.rotation, null);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            canCtrl = false;            
        }
        
        if (Setting.setting&&Setting.setting.gameObject.activeSelf) timeScale = 0;
    }

    /// <summary>
    /// 因视野消失而结束游戏
    /// </summary>
    private void OverByLeaveView()
    {
        Vector2 currentPos = Camera.main.WorldToScreenPoint(transform.position);


        //if (Input.GetMouseButtonDown(0))
        //{
        //    print(Screen.width / 2.0f);
        //    print(Screen.height / 2.0f);
        //    print(Input.mousePosition);
        //    print("--------");
        //}


        //print("-------");
        if ((currentPos.x < 0 || currentPos.x > Screen.width ||
            currentPos.y < 0 || currentPos.y > Screen.height) && Winprogress == 0)
        {
            isInVisibel = true;
        }
        else
        {
            isInVisibel = false;
        }
        if(isInVisibel)
        {
            inVisibleTimer += Time.deltaTime * Shuttle.shuttle.speedRate;
            if(inVisibleTimer>inVisibleTime)
            {
                End.EndGame(false);
            }
        }
        else
        {
            
            inVisibleTimer = 0;
        }
    }
    //private void OnBecameInvisible()
    //{
    //    isInVisibel = true;
    //    print("inInvisible");
    //}
    //private void OnBecameVisible()
    //{
    //    print("在视野内");
    //}
    
}
