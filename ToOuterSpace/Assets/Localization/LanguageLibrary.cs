using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 存储 标记 和 翻译
/// </summary>
namespace LocalizationSystem
{
    /// <summary>
    /// 存储某一个标记及对应的翻译
    /// </summary>
    [System.Serializable]    
    public class Pair
    {
        public string name;
        public string value;

        public Pair(string name, string value)
        {
            this.name = name;
            this.value = value;
        }
        /// <summary>
        /// 将一行文本（标记 和 翻译）进行切割
        /// </summary>
        /// <param name="str">一行文本（标记 和 翻译）</param>
        /// <returns>返回存储了标记和翻译的Pair类</returns>
        public static Pair ToObject(string str)
        {
            string[] splits = str.Split(' ');
            if (splits.Length < 2)
            {
                Debug.LogError("该词条并未以空格分割！");
                return null;
            }
            else
            {
                for (int i = 2; i < splits.Length; i++)
                {
                    splits[1] += " " + splits[i];
                }
                return new Pair(splits[0], splits[1]);
            }
        }
        /// <summary>
        /// 将 标记 和 翻译 进行一行显示
        /// </summary>
        /// <param name="pair">存储了标记和翻译的 Pair类</param>
        /// <returns>以string形式显示</returns>
        public static string ToString(Pair pair)
        {
            return pair.name + " " + pair.value + "\n";
        }
    }
    /// <summary>
    /// 存储所有当前的标记及对应的翻译
    /// </summary>
    public class LanguageLibrary
    {
        public static LanguageLibrary current;

        /// <summary>
        /// 所有标记和翻译的存储
        /// </summary>
        public static List<Pair> pairs;
        

        public LanguageLibrary(TextAsset txt)
        {
            string[] splits = txt.text.Split('\n');
            pairs = new List<Pair>();
            for (int i = 0; i < splits.Length; i++)
            {
                if (splits[i].Contains(" ") && splits[i][0] != '/')
                {
                    Pair pair = Pair.ToObject(splits[i]);
                    if (pair != null) pairs.Add(pair);
                }
            }
            pairs.Sort((x, y) => string.Compare(x.name, y.name));
        }

        /// <summary>
        /// 根据标记找到所有对应翻译
        /// </summary>
        /// <param name="name">标记</param>
        /// <returns>存在则返回翻译，否则返回标记</returns>
        public static string FindTranslation(string name)
        {
            if (current == null)
            {
                Debug.LogError("当前不存在对应翻译库！");
                return name;
            }
            else
            {
                if (name == "") return "";
                foreach(Pair pair in pairs)
                    if (pair.name == name) return pair.value;

                Debug.LogError("当前翻译库中不存在对应词条:" + name + "！");
                return name;
            }
        }
    }

}