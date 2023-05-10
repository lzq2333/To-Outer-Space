  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制特效存在时间
/// </summary>
public class EffectTimer : MonoBehaviour
{
    [Header("存在时间")]
    public float time = 5;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
