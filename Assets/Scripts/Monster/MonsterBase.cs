using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBase : MonoBehaviour
{   
    private SpriteRenderer spriteRenderer;//控制怪物的图片切换
    private Animator animator;//怪物的动画控制器
    
    private bool isDead;//用来判断怪物是否已经死亡
    private bool isDay;//怪物处在白天还是黑夜


    public MonsterDataSO monsterData;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    #region 转换白天黑夜模式
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
