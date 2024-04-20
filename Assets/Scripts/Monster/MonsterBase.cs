using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBase : MonoBehaviour
{   
    private SpriteRenderer spriteRenderer;//���ƹ����ͼƬ�л�
    private Animator animator;//����Ķ���������
    
    private bool isDead;//�����жϹ����Ƿ��Ѿ�����
    private bool isDay;//���ﴦ�ڰ��컹�Ǻ�ҹ


    public MonsterDataSO monsterData;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    #region ת�������ҹģʽ
    public void TransToDay()
    {
        isDay = true;
        spriteRenderer.sprite = monsterData.daySprite;
    }

    public void TransToNight()
    {
        isDay = false;
        spriteRenderer.sprite = monsterData.nightSprite;
    }
    #endregion


}
