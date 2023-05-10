using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondTitleManger : MonoBehaviour
{
    public int secondTitleCount = 5;
    private RectTransform secondTitleParent;

    private List<SecondTitle> secondTitles = new List<SecondTitle>();
    private void Awake()
    {
        secondTitleParent = transform.GetChild(0).GetComponent<RectTransform>();

        //计量高度
        secondTitleParent.sizeDelta = new Vector2(secondTitleParent.sizeDelta.x, 80 * secondTitleCount-30+10);
        
        for (int i = 0; i < secondTitleParent.childCount; i++)
        {
            if (i >= secondTitleCount)
            {
                Destroy(secondTitleParent.GetChild(i).gameObject);
            }
            else
            {
                secondTitles.Add(secondTitleParent.GetChild(i).GetComponent<SecondTitle>());
            }
            
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
       if (secondTitles[0].titleType == MainTitleType.Knowledge)
        {
            DataBase.Instacne.UpdateKnowledgeMangers();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
