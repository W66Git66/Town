using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scarecrow : MonoBehaviour
{
    private GameObject clickE;
    private bool isTrigger=false;
    private int live, dead;
    public GameObject jiaoFu;
    public GameObject followPlayer;
    private Transform texiao;
    public GameObject ScarecrowYinDao;

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
        live = DataSaveManager.Instance.liveBird;
        dead = DataSaveManager.Instance.deadBird;
    }

    private void Start()
    {
        clickE = transform.GetChild(1).gameObject;
        clickE.SetActive(false);
        jiaoFu.SetActive(false);

        TransState(EnemyStates.Idle);
    }

    private void Update()
    {
        
        if (isTrigger)
        {
            if (DataSaveManager.Instance.deadBird != 0 || DataSaveManager.Instance.liveBird != 0)
            {
                clickE.SetActive(true);
                jiaoFu.GetComponent<JiaoFu>().scarecrow_ = this;               
                if (Input.GetKeyDown(KeyCode.E))
                {
                    jiaoFu.SetActive(true);
                }
                
            }
        }
        
            if (DataSaveManager.Instance.isScareDes && !DataSaveManager.Instance.isFirstChuMoScare)
            {
                DataSaveManager.Instance.isFirstChuMoScare = true;
                GameManager.Instance.TanChuangScare();
            }
        
    }
    private void FixedUpdate()
    {
        StateMachine();
    }

    public void ChuMoScare()
    {
        clickE.SetActive(false);
        DataSaveManager.Instance.GetLiveBird();
        TransState(EnemyStates.Death);
        if (texiao != null)
        {
            texiao.gameObject.SetActive(false);
        }
        
        if (DataSaveManager.Instance.isScareBeated == false)
        {
            DataSaveManager.Instance.TransPoints();
        }
        DataSaveManager.Instance.isScareBeated = true;
        followPlayer.GetComponent<DialogueSysYinDao>().ChuMoScareYinDaoVar();

    }



    private void StateMachine()
    {
        switch (curState)
        {
            
            case EnemyStates.Death:
                Destroy(gameObject,1.5f);
                anim.SetTrigger("IsBird");
                GameManager.Instance.ChangeAudioClip(GameManager.Instance.chuMo);
                GameManager.Instance.PlaySound();
                
                PlayerController.Instance.speed = 10.0f;

                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (curState != EnemyStates.Death && collision.CompareTag("Player"))
        {
            live = DataSaveManager.Instance.liveBird;
            dead = DataSaveManager.Instance.deadBird;
            isTrigger = true;
            PlayerController.Instance.speed *= 0.7f;
            texiao = collision.transform.GetChild(2);
            texiao.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (curState != EnemyStates.Death && collision.CompareTag("Player"))
        {   
            isTrigger= false;
            PlayerController.Instance.speed /= 0.7f;
            texiao.gameObject.SetActive(false);
            clickE.SetActive(false);
        }
    }
    private void TransState(EnemyStates states)
    {
        curState = states;
    }

}
