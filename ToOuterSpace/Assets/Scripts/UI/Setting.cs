using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 设置
/// </summary>
public class Setting : MonoBehaviour
{
    public static Setting setting;
    private InputField input_force;

    private void Awake()
    {
        input_force = transform.Find("Force/InputField").GetComponent<InputField>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        input_force.onValueChanged.AddListener(GameManger.Instance.OnForceChange);
        setting = this;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set()
    {
        if (DataBase.Instacne == null || DataBase.Instacne.gameObject.transform.localScale == Vector3.zero)
        {
            gameObject.SetActive(true);
        }
        
        //input_force.text = GameManger.Instance.Force.ToString();
        //if(SceneManager.GetActiveScene().name=="Start"|| SceneManager.GetActiveScene().name == "LevelSelect")
        //{
        //    transform.Find("Current").gameObject.SetActive(false);
        //    transform.Find("BackGame").gameObject.SetActive(false);
        //}
    }
    /// <summary>
    /// 重新开始游戏
    /// </summary>
    public void Restart()
    {
        //End.EndGame(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
