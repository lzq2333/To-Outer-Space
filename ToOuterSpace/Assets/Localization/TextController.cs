using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 进行文本的翻译替换
/// </summary>
public class TextController : MonoBehaviour
{
    Text text;
    [SerializeField]
    //文本类型(cns/eng)
    string language = "cns";
    [Header("翻译对应的标记")]
    public string identifier;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.text = Localization.FindTranslation(identifier);
    }

    // Update is called once per frame
    void Update()
    {
        if(language != Localization.language)
        {
            language = Localization.language;
            text.text = Localization.FindTranslation(identifier);
        }
    }
}
