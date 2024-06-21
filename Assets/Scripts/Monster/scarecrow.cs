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
        Transform clickETransform= transform.Find("E");
        clickE = clickETransform.gameObject;
        clickE.SetActive(false);
        jiaoFu.SetActive(false);

        TransState(EnemyStates.Idle);
    }

    private void Update()
    {
        StateMachine();

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
        if (ScarecrowYinDao!= null)
        { 
            if (ScarecrowYinDao.GetComponent<NPC>().isOver && DataSaveManager.Instance.isFirstChuMoScare)
            {
                DataSaveManager.Instance.isFirstChuMoScare = true;
                GameManager.Instance.TanChuangScare();
            }
        }
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

    private void FixedUpdate()
    {

    }

    private void StateMachine()
    {
        switch (curState)
        {
            
            case EnemyStates.Death:

               anim.SetTrigger("IsBird");
                GameManager.Instance.ChangeAudioClip(GameManager.Instance.chuMo);
                GameManager.Instance.PlaySound();
                Destroy(gameObject, 1.5f);
                PlayerController.Instance.speed = 10.0f;

                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        live = DataSaveManager.Instance.liveBird;
        dead = DataSaveManager.Instance.deadBird;
        isTrigger = true;
        if (curState != EnemyStates.Death && collision.CompareTag("Player"))
        {
            PlayerController.Instance.speed *= 0.7f;
            texiao = collision.transform.GetChild(2);
            texiao.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTrigger= false;
        if (curState != EnemyStates.Death && collision.CompareTag("Player"))
        {
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
