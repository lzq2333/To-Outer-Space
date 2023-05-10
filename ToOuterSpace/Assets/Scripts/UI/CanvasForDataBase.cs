using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasForDataBase : MonoBehaviour
{
    private static CanvasForDataBase _instance;

    public static CanvasForDataBase Instance { get => _instance; private set => _instance = value; }

    void Awake()
    {
        if(Instance ==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
