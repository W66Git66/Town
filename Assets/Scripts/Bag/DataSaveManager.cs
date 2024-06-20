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

    [Header("�Ƿ��ٻؼ��⿵")]
    public bool isJirouBack = false;

    [Header("�Ƿ����紫��")]
    public bool isShenSheBack = false;

    [Header("�Ƿ���й�����ҹ��Ի�")]
    public bool isXinshouGuanYinDao = false;

    [Header("�Ƿ���й��峤�����Ի�")]
    public bool isCunZhangYinDao=false;

    [Header("�Ƿ���й����κ�ҹ����")]
    public bool isRuMengYinDao = false;

    [Header("�Ƿ��һ�γ�ħ��")]
    public bool isChuMoGou = false;

    [Header("�Ƿ��һ���õ���ȸ")]
    [Header("�Ƿ��һ�γ�ħ������")]
    [Header("�Ƿ��һ�γ�ħ��")]

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
        GameManager.Instance.checkPoints = checkPoints1;
    }

    public void NightReSet()//ҹ��ˢ��
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


    public void TransPoints()
    {
        GameManager.Instance.checkPoints = checkPoints2;
    }
    public void SetRuMengYinDao()
    {
        isRuMengYinDao= true;
    }
}
