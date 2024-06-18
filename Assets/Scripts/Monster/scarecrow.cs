using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scarecrow : MonoBehaviour
{
    [SerializeField] private float _speed;//怪物的移动速度

    private EnemyStates curState;

    //private float Timer = 0;//待机计时器

    [HideInInspector] public SpriteRenderer sr;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Collider2D enemyCollider;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();//碰撞器组件
    }

    private void Start()
    {
        TransState(EnemyStates.Idle);
    }

    private void Update()
    {
        StateMachine();
    }

    private void FixedUpdate()
    {

    }

    private void StateMachine()
    {
        switch (curState)
        {
            
            case EnemyStates.Death:

               // anim.Play("null");
                //加一个图片淡出效果

                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (curState != EnemyStates.Death && collision.CompareTag("蛋白粉"))
        {
            if (DataSaveManager.Instance.isDog == false)
            {
                DataSaveManager.Instance.isDog = true;
            }
            TransState(EnemyStates.Death);
        }
        if(curState != EnemyStates.Death && collision.CompareTag("Player"))
        {
            PlayerController.Instance.speed *= 0.7f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (curState != EnemyStates.Death && collision.CompareTag("Player"))
        {
            PlayerController.Instance.speed /= 0.7f;
        }
    }
    private void TransState(EnemyStates states)
    {
        curState = states;
    }

}
