using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 存储星球信息
/// </summary>
public class StarManager : MonoBehaviour
{
    public static StarManager main;

    public static Planet[] Planets
    {
        get
        {
            return main.GetComponentsInChildren<Planet>();
        }
    }

    private void Awake()
    {
        main = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
