using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 语言
/// </summary>
public class Language : MonoBehaviour
{
    public static string language = "cns";
    //Text text;

    // Start is called before the first frame update
    void Start()
    {

        if(language != GameManger.Instance.currentLanguage)
        {
            Change();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Change()
    {
        if (language == "cns")
        {
            //text.text = "English";
            language = "eng";
            
            Localization.ChangeLanguage("eng");
            GameManger.Instance.currentLanguage = "eng";
            //PlayerPrefs.SetString(SaveHash.Language, "eng");
            GameManger.Instance.SaveByJson();
        }
        else
        {
            //text.text = "简体中文";
            language = "cns";
            
            Localization.ChangeLanguage("cns");
            GameManger.Instance.currentLanguage = "cns";
            GameManger.Instance.SaveByJson();
            //PlayerPrefs.SetString(SaveHash.Language, "cns");
        }
    }
}
