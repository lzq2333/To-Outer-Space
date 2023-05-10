using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// 进行音量控制
/// </summary>
public class Volume : MonoBehaviour
{
    public string type;
    public float volume;
    public AudioMixer mixer;
    private Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if(type=="Music")
        {
            SetVolume(GameManger.Instance.MusicVolume);
            slider.value = GameManger.Instance.MusicVolume;
        }
        else if(type=="Effect")
        {
            SetVolume(GameManger.Instance.EffectVolume);
            slider.value = GameManger.Instance.EffectVolume;
        }
        
        //SetVolume(0.6f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 改变音量
    /// </summary>
    /// <param name="v">音量值</param>
    public void SetVolume(float v)
    {
        
        if (v <= 0.01f)
        {
            mixer.SetFloat(type, -80);
            return;
        }

        mixer.SetFloat(type, (int)(v * 50 - 40));
        if (type == "Music")
        {
            GameManger.Instance.MusicVolume = v;
        }
        else if (type == "Effect")
        {
            
            GameManger.Instance.EffectVolume = v;
        }
    }
    
}
