using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ÔÝÍ£
/// </summary>
public class Pause : MonoBehaviour
{
    private Button btn_pause;
    [SerializeField]
    private Sprite playSprite;
    [SerializeField]
    private Sprite pauseSprite;
    
    private bool isPause;
    private Image img_pause;
    public bool IsPause { private set => isPause = value; get => isPause; }
    public static Pause Instance { get => _instance;private set => _instance = value; }

    private static Pause _instance; 
    private void Awake()
    {
        Instance = this;
        IsPause = false;
        btn_pause = GetComponent<Button>();
        img_pause = GetComponent<Image>();
        btn_pause.onClick.AddListener(OnPauseButtonClick);
    }

    //µã»÷ÊÂ¼þ
    private void OnPauseButtonClick()
    {
        if(DataBase.Instacne)
        {
            if(DataBase.Instacne.transform.localScale!=Vector3.zero)
            {
                return;
            }
        }
        IsPause = !IsPause;
        if(IsPause)
        {
            Time.timeScale = 0;
            img_pause.sprite = playSprite;
        }
        else
        {
            Time.timeScale = 1;
            img_pause.sprite = pauseSprite;
        }
    }
}
