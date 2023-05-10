using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowImg : MonoBehaviour
{
    private Image img;
    
    [SerializeField]
    private Sprite canSprite;
    [SerializeField]
    private Sprite disSprite;
    private Vector2 localPos;

    
    private void Awake()
    {
        img = GetComponentInChildren<Image>();
    }
    void Start()
    {
        Shuttle.shuttle.MaxCtrlDiatance = transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x/2;
    }
    private void Update()
    {
        if(Shuttle.shuttle && Shuttle.shuttle.canCtrl)
        {
            img.enabled = true;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
             GetComponent<RectTransform>(),
             Camera.main.WorldToScreenPoint(Shuttle.shuttle.transform.position),
             Camera.main,
             out localPos
             ) ;

            transform.GetChild(0).localPosition = localPos;


            Vector2 mousePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                GetComponent<RectTransform>(),
                Input.mousePosition,
                Camera.main,
                out mousePos
                );
            float ctrlDistance = Vector2.Distance(mousePos, transform.GetChild(0).localPosition);
            
            Shuttle.shuttle.ctrlDistance = ctrlDistance;
            Shuttle.shuttle.delta= (Vector2)transform.GetChild(0).localPosition-mousePos;
            Shuttle.shuttle.delta *= 2;
            if (ctrlDistance<Shuttle.shuttle.MaxCtrlDiatance-Shuttle.shuttle.engleDistance)
            {
                img.sprite = canSprite;
            }
            else
            {
                img.sprite = disSprite;
            }
            
            
        }
        else
        {
            GetComponentInChildren<Image>().enabled = false;
        }
        
    }
}
