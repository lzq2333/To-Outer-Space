using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartPlants : MonoBehaviour
{
    private readonly float LeftRange= -Screen.width / 2;
    private readonly float RightRange = Screen.width / 2;
    private readonly float TopRange = Screen.height / 2;
    private readonly float BottomRange = -Screen.height / 2;
    [SerializeField]
    private List<Sprite> plantSprites = new List<Sprite>();
    private List<Tweener> plantMoves = new List<Tweener>();
    private Transform[] plants;
    /// <summary>
    /// 星球飞行速度
    /// </summary>
    [Header("星球飞行速度")]
    public float speed = 1;
    private void Awake()
    {
        plants = new Transform[transform.childCount];
        for(int i=0;i<transform.childCount;i++)
        {
            plants[i] = transform.GetChild(i).transform;
        }
        
        foreach(Transform plant in plants)
        {
            print(plant.gameObject.name);
            plant.GetComponent<Image>().sprite = plantSprites[Random.Range(0, plantSprites.Count)];
        }
        
    }
    private void Start()
    {
        foreach (Transform plant in plants)
        {
            float requireTime;
            Vector2 disPosition = GetRandomPosition(Vector2.zero, out requireTime);
            Tweener tweener = plant.DOLocalMove(disPosition, requireTime);
            tweener.OnComplete(()=> { PlantMove(plant); });//.OnComplete<Transform>(PlantMove(plant));
            //plantMoves.Add(tweener);
        }
    }
    private void Update()
    {
        
        //for(int i=0;i<plantMoves.Count;i++)
        //{
        //    if(plantMoves[i].IsComplete())
        //    {
                             
        //    }
        //}
    }
    private void PlantMove(Transform trans)
    {
        float requireTime;
        Vector2 disPosition = GetRandomPosition(trans.localPosition, out requireTime);
        trans.DOLocalMove(disPosition, requireTime).OnComplete(()=> { PlantMove(trans); });
    }
    
    /// <summary>
    /// 根据现在位置随机生成新位置
    /// </summary>
    /// <param name="currentPos">当前位置</param>
    /// <param name="time">运动所需要的时间</param>
    /// <returns>目标位置</returns>
    private Vector2 GetRandomPosition(Vector2 currentPos,out float time)
    {
        Vector2 pos = currentPos;
        if (currentPos.x < LeftRange)
        {
            pos.x += Random.Range(0, RightRange);
        }
        else if (currentPos.x > RightRange)
        {
            pos.x += Random.Range(LeftRange, 0);
        }
        else
        {
            pos.x += Random.Range(LeftRange, RightRange);
        }
        if (currentPos.y < BottomRange)
        {
            pos.y += Random.Range(0, TopRange);
        }
        else if (currentPos.y > TopRange)
        {
            pos.y += Random.Range(BottomRange, 0);
        }
        else
        {
            pos.y += Random.Range(BottomRange, TopRange);
        }
        time = Vector2.Distance(pos, currentPos) * Random.Range(0.5f, 1f) / speed;
        return pos;
    }
}
