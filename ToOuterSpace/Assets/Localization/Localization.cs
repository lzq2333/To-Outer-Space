using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LocalizationSystem;

/// <summary>
/// 读取文档的内容
/// </summary>
public class Localization
{
    public static string language = "cns";
    public static Localization main;


    static Localization()
    {
        main = new Localization();
        ChangeLanguage(language);

    }
    /// <summary>
    /// 将LanguageLibrary进行重新的词汇存储
    /// </summary>
    /// <param name="language">要变成的语言（填存储文件名cns/eng）</param>
    public static void ChangeLanguage(string language)
    {
        Localization.language = language;
        TextAsset txt = Resources.Load("Localization/" + language) as TextAsset;
        if (txt != null)
            LanguageLibrary.current = new LanguageLibrary(txt);

    }
    /// <summary>
    ///根据标记找到对应翻译 
    /// </summary>
    /// <param name="str">标记</param>
    /// <returns>返回对应翻译</returns>
    public static string FindTranslation(string str)
    {
        return LanguageLibrary.FindTranslation(str);
    }
}
