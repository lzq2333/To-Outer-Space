using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Video;

public class Once : MonoBehaviour
{
    [SerializeField]
    private GameObject tipButton;
    [SerializeField]
    private GameObject tip;
    [SerializeField]
    private Button close;


    public Camera miniCamera;

    private bool isBigger = false;
    private bool isSmaller = false;

    private static Once _instance;
    public Text txt_test;
    public static Once Instance { get => _instance; private set => _instance = value; }

    private void Awake()
    {
        Instance = this;
        
        DataBase.Instacne.transform.parent.GetComponent<Canvas>().worldCamera = Camera.main;
        DataBase.Instacne
            .target = tipButton.transform;
    }
    private void Start()
    {
        if(miniCamera!=null)
        {
            Shuttle.shuttle.miniCamera = miniCamera;
        }

        int tempLevel = 0;

        if (SceneManager.GetActiveScene().name.StartsWith("Level"))
        {
            tempLevel = int.Parse(SceneManager.GetActiveScene().name.Remove(0, 5));
        }


        if (txt_test != null)
        {
            txt_test.text = "2";
        }
        //ÅÐ¶ÏÊÇ·ñ²úÉúTipButton;
        if (txt_test != null)
        {
            txt_test.text = "1";
        }

        tipButton.SetActive(true);
        tipButton.GetComponentInChildren<Button>().onClick.AddListener(OnTipButtonClick);


        close?.onClick.AddListener(() => { isSmaller = true; Tip.Instance.StopVideo(); });
        GameManger.Instance.OnSceneChange(tempLevel);
    }
    private void OnTipButtonClick()
    {
        DataBase.Instacne.Show();
    }
    private void Update()
    {
        ScaleChange();
    }
    private void ScaleChange()
    {
        if(isBigger)
        {
            tip.transform.localScale = Vector3.Lerp(tip.transform.localScale,
                Vector3.one, 5 * Time.unscaledDeltaTime);
            if(tip.transform.localScale.x>0.95f)
            {
                Tip.Instance.PlayVideo();
                isBigger = false;
            }
            return;
        }
        if(isSmaller)
        {
            tip.transform.localScale = Vector3.Lerp(tip.transform.localScale,
                Vector3.zero, 5 * Time.unscaledDeltaTime);
            if (tip.transform.localScale.x < 0.05f)
            {
                tip.SetActive(false);
                isSmaller = false;
                Time.timeScale = 1;
                
            }
            return;
        }
    }
    public void OncePlay()
    {
        tip.SetActive(true);        
        Time.timeScale = 0;
        Tip.Instance.PlayVideo();
    }

    
}
