using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaveManager : MonoBehaviour
{
    //public PlayerBag bag = new PlayerBag();

    [Header("死去的麻雀数量")]
    public int deadBird=0;

    [Header("活蹦乱跳的麻雀数量")]
    public int liveBird=0;

    [Header("是否持有煞气刀")]
    public bool isKnifeOn=false;

    [Header("是否找到假牙")]
    public bool isFakeToothFind=false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void GetDeadBird()//获得死去的麻雀
    {
        deadBird++;
    }
    public void UseDeadBird()//使用死去的麻雀
    {
        deadBird--;
    }

    public void GetLiveBird()//获得活蹦乱跳的麻雀
    {
        if (deadBird <= 0)
        {
            return;
        }
        else if (deadBird > 0)
        {
            liveBird = deadBird;
        }
    }
    public void UseLiveBird()//使用活蹦乱跳的麻雀
    {
        if(liveBird <= 0)
        {
            return;
        }
        else if(liveBird > 0)
        {
            liveBird--;
        }
    }

    public void GetKnife()//获得煞气刃
    {
        isKnifeOn = true;
    }

    public void NightReSet()//夜晚刷新
    {
        deadBird = liveBird = 0;
    }
}
