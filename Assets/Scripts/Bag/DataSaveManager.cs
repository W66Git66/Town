using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaveManager : Singleton<DataSaveManager>
{
    //public PlayerBag bag = new PlayerBag();
    public List<Transform> checkPoints0;
    public List<Transform> checkPoints1;
    public List<Transform> checkPoints2;
    public List<Transform> checkPoints3;

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

    [Header("是否召回肌肉康")]
    public bool isJirouBack = false;

    [Header("是否神社传回")]
    public bool isShenSheBack = false;

    [Header("是否进行过新手夜晚对话")]
    public bool isXinshouGuanYinDao = false;

    [Header("是否进行过村长引导对话")]
    public bool isCunZhangYinDao=false;

    [Header("是否进行过入梦黑夜引导")]
    public bool isRuMengYinDao = false;

    [Header("是否第一次除魔狗")]
    public bool isChuMoGou = false;

    [Header("是否第一次拿到麻雀")]
    [Header("是否第一次除魔稻草人")]
    [Header("是否第一次除魔猪")]

    public bool isDog=false;

    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        GameManager.Instance.checkPoints = checkPoints0;
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
        GameManager.Instance.checkPoints = checkPoints1;
    }

    public void NightReSet()//夜晚刷新
    {
        deadBird = liveBird = 0;
    }

    public void ShenSheBack()
    {
        isShenSheBack = true;
    }

    public void UnShenSheBack()
    {
        isShenSheBack= false;
    }

    public void SetXinshouGuanYinDao()
    {
        isXinshouGuanYinDao= true;
    }

    public void SetCunZhangYinDao()
    {
        isCunZhangYinDao= true;
    }

<<<<<<< HEAD
    public void TransPoints()
    {
        GameManager.Instance.checkPoints = checkPoints2;
=======
    public void SetRuMengYinDao()
    {
        isRuMengYinDao= true;
>>>>>>> d849bde9ee35d24b7d2ec6dc5dbe9a3f5fb40ffb
    }
}
