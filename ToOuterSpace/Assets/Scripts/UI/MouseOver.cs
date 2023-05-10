using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isBig = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        isBig = true;
        GetComponent<RectTransform>().localScale *= 1.2f;
    }
    private void OnDisable()
    {
        if(isBig)
        {
            GetComponent<RectTransform>().localScale /= 1.2f;
            isBig = false;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        isBig = false;
        GetComponent<RectTransform>().localScale /= 1.2f;
    }
}
