using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scarecrow : MonoBehaviour
{
    private GameObject clickE;
    private bool isTrigger=false;
    private int live, dead;
    public GameObject jiaoFu;

    [SerializeField] private float _speed;//������ƶ��ٶ�

    private EnemyStates curState;

    //private float Timer = 0;//������ʱ��

    [HideInInspector] public SpriteRenderer sr;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Collider2D enemyCollider;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();//��ײ�����
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
                if (Input.GetKeyDown(KeyCode.E))
                {
                    jiaoFu.SetActive(true);
                }
            }

            if (live != DataSaveManager.Instance.liveBird || dead != DataSaveManager.Instance.deadBird)
            {
                clickE.SetActive(false);
                TransState(EnemyStates.Death);
                DataSaveManager.Instance.GetLiveBird();
                DataSaveManager.Instance.isScareBeated = true;
            }
        }
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
                Destroy(gameObject, 1.5f);
                PlayerController.Instance.speed = 10.0f;
                //��һ��ͼƬ����Ч��

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
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTrigger= false;
        if (curState != EnemyStates.Death && collision.CompareTag("Player"))
        {
            PlayerController.Instance.speed /= 0.7f;
            clickE.SetActive(false);
        }
    }
    private void TransState(EnemyStates states)
    {
        curState = states;
    }

}
