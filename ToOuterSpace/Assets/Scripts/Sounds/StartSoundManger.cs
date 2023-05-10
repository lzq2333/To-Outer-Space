using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// �������ֿ�ʼʱ������
/// </summary>
public class StartSoundManger : MonoBehaviour
{
    [SerializeField]
    private AudioMixer mixer;
    
    [SerializeField]
    [Header("Ŀ��������С")]
    [Range(0,1)]
    private float desVolume=0.6f;
    //��ǰ������С
    private float currentVolume = 0;
    /// <summary>
    /// �����ٶ�
    /// </summary>
    [Header("�����ٶ�")]
    [Range(0,1)]
    public float speed=0.2f;
    
    private void Start()
    {
        desVolume = GameManger.Instance.MusicVolume;
        if (GameManger.Instance.isFirst)
        {
            mixer.SetFloat("Music", -70);
        }
    }
    private void Update()
    {
        if(GameManger.Instance.isFirst && currentVolume < desVolume)
        {
            currentVolume += Time.deltaTime*speed;
            mixer.SetFloat("Music", currentVolume * 50 - 40);//-80~-30     0~0.4
            GameManger.Instance.MusicVolume = desVolume;            
            if(currentVolume==0.3f)
            {
                speed /=2f;
            }
        }
        else
        {
            GameManger.Instance.isFirst = false;
        }
    }
    private void OnDestroy()
    {
        if(!GameManger.Instance.isFirst)
        {
            return;
        }
        GameManger.Instance.isFirst = false;
        GameManger.Instance.MusicVolume = currentVolume;
        
        
    }
}
