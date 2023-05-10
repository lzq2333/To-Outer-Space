
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 摄像机跟随
/// </summary>

public class Follow : MonoBehaviour
{   
    [SerializeField]
    [Header("跟随目标")]
    Transform follow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (follow != null)
        {
            Vector3 pos = follow.position;
            pos.z = transform.position.z;
            transform.position = pos;
        }
    }
}
