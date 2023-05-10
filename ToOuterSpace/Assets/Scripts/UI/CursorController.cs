using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField]
    private Texture2D cursor_normal;
    [SerializeField]
    private Texture2D cursor_ctrl;

    void Update()
    {
        if(Shuttle.shuttle && Shuttle.shuttle.canCtrl)
        {
            Cursor.SetCursor(cursor_ctrl, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(cursor_normal, Vector2.zero, CursorMode.Auto);
        }

    }

}
