using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    
    private Dictionary<Button, float> speedDic;
    private Button btn_current;
    private float speed = 1;
    private static TimeController _instance;
    /// <summary>
    /// 当前速度
    /// </summary>
    public float Speed { get => speed;private set => speed = value; }
    public static TimeController Instance { get => _instance; private set => _instance = value; }

    private void Awake()
    {
        Instance = this;
        speedDic = new Dictionary<Button, float>();
        for (int i = 0; i < transform.childCount; i++)
        {
            if(!speedDic.ContainsKey(transform.GetChild(i).GetChild(0).GetComponent<Button>()))
            {
                speedDic.Add(transform.GetChild(i).GetChild(0).GetComponent<Button>(), float.Parse(transform.GetChild(i).name));
                transform.GetChild(i).GetChild(0).GetComponent<Button>().onClick.AddListener(OnSpeedButtonClick);
            }
        }
        
    }
    private void Start()
    {
        Shuttle.shuttle.speedRate = 1;
        btn_current = transform.Find("1").GetChild(0).GetComponent<Button>();
    }
    private void OnSpeedButtonClick()
    {
        if(Pause.Instance.IsPause)
        {
            return;
        }
        Button btn_current = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        if(speedDic.ContainsKey(btn_current))
        {
            Shuttle.shuttle.speedRate = speedDic[btn_current];
            Speed = speedDic[btn_current];
            Image[] imgs = this.btn_current.transform.parent.GetComponentsInChildren<Image>();//.color = Color.white;
            //print(imgs.Length);
            for(int i=0;i<imgs.Length;i++)
            {
                if(imgs[i].gameObject.GetComponent<Button>()!=null)
                {
                    continue;
                }
                imgs[i].color = Color.white;
            }
            Image[] imgs_new = btn_current.gameObject.transform.parent.GetComponentsInChildren<Image>();
            for (int i = 0; i < imgs_new.Length; i++)
            {
                if (imgs_new[i].gameObject.GetComponent<Button>() != null)
                {
                    continue;
                }
                imgs_new[i].color = Color.yellow;
            }
            this.btn_current = btn_current;
        }
        else
        {
            Debug.LogError($"没有找到按键{btn_current.name}");
        }
    }
}
