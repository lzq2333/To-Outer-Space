using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI 取消按键
/// </summary>
public class Cancel : MonoBehaviour
{

    private static Cancel _instance;
    [SerializeField]
    Image image;

    public static Cancel Instance { get => _instance; private set => _instance = value; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //当图片的a值低于0.3时将不会接收鼠标射线检测
        image.alphaHitTestMinimumThreshold = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        //image.gameObject.SetActive(Input.GetMouseButton(0) && !Setting.setting.gameObject.activeSelf);
        //image.gameObject.SetActive(Input.GetMouseButton(0) && !Setting.setting.gameObject.activeSelf && 
        //    !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject());
        image.gameObject.SetActive(Shuttle.shuttle.canCtrl && Input.GetMouseButton(0));
    }
    public float MouseDistanceToUI()
    {
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.GetComponent<RectTransform>(),
            Input.mousePosition, Camera.main, out localPos);
        
        return Vector2.Distance(localPos, transform.GetChild(0).transform.position);
    }
}
