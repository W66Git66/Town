using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaveManager : Singleton<DataSaveManager>
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

    [Header("是否找到过蛋白粉")]
    public bool isProteinEverbeenFound=false;

    [Header("是否消灭过稻草人")]
    public bool isScareBeated = false;

    public bool isDog=false;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();   
    }

    public void GetDeadBird(int birdnum)//获得死去的麻雀
    {
        deadBird+=birdnum;
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

    public void FindFakeTooth()
    {
        isFakeToothFind = true;
    }

    public void FindProtein()
    {
        isProteinEverbeenFound = true;
    }

    public void NightReSet()//夜晚刷新
    {
        deadBird = liveBird = 0;
    }
}
