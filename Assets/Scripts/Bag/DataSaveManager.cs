using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaveManager : Singleton<DataSaveManager>
{
    //public PlayerBag bag = new PlayerBag();

    [Header("��ȥ����ȸ����")]
    public int deadBird=0;

    [Header("�����������ȸ����")]
    public int liveBird=0;

    [Header("�Ƿ����ɷ����")]
    public bool isKnifeOn=false;

    [Header("�Ƿ��ҵ�����")]
    public bool isFakeToothFind=false;

    [Header("�Ƿ��ҵ������׷�")]
    public bool isProteinEverbeenFound=false;

    [Header("�Ƿ������������")]
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

    public void GetDeadBird(int birdnum)//�����ȥ����ȸ
    {
        deadBird+=birdnum;
    }
    public void UseDeadBird()//ʹ����ȥ����ȸ
    {
        deadBird--;
    }

    public void GetLiveBird()//��û����������ȸ
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
    public void UseLiveBird()//ʹ�û����������ȸ
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

    public void GetKnife()//���ɷ����
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

    public void NightReSet()//ҹ��ˢ��
    {
        deadBird = liveBird = 0;
    }
}
