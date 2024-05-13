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

    public bool isDog=false;
    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();   
    }

    public void GetDeadBird()//�����ȥ����ȸ
    {
        deadBird++;
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

    public void NightReSet()//ҹ��ˢ��
    {
        deadBird = liveBird = 0;
    }
}