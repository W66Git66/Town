using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Ghost : MonoBehaviour
{
    [SerializeField] private float _speed;//������ƶ��ٶ�

    private EnemyStates curState;

    [Header("Ŀ��")]
    public Transform player;

    [Header("����Ѳ��")]
    public float IdleDuration = 0.5f; //����ʱ��
    public Transform[] patrolPoints;//Ѳ�ߵ�
    public int targetPointIndex = 0;//Ŀ�������

    [Header("�ƶ�׷��")]
    public float currentSpeed = 0;
    public Vector2 MovementInput { get; set; }

    public float chaseDistance = 3f;//׷������


    [Header("Pathfinding")]
    private Seeker _seeker;
    private List<Vector3> _pathPoints;//·����
    private int _curIndex;//·���������
    [SerializeField] private float pathFindCooldown = 0.5f;//��Ѱ·������ȴ
    private float pathTimer = 0;//��ʱ��

    [Header("����")]
    [HideInInspector] public float distance;
    public LayerMask playerLayer;//��ʾ���ͼ��

    private float Timer = 0;//������ʱ��

    [HideInInspector] public SpriteRenderer sr;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Collider2D enemyCollider;

    private float stopTime = 0f;//����ֹͣ�˶���ʱ��
    private float stopThreshold = 3f;//ֹͣ����ֵ

    private bool isPatrol = false;

    public GameObject bullet;
    float shootTime = 2f;

    public GameObject followPlayer;
    public GameObject ghostYinDao;

    private void Awake()
    {
        _seeker = GetComponent<Seeker>();

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();//��ײ�����
    }

    private void Start()
    {
        TransState(EnemyStates.Idle);
    }

    private void Update()
    {
        if (curState != EnemyStates.Death)
        {
            GetPlayerTransform();

           // AutoPath();
        }

        if(ghostYinDao != null)
        {
            if (ghostYinDao.GetComponent<NPC>().isOver && !DataSaveManager.Instance.isGhostTanChuang)
            {
                DataSaveManager.Instance.isGhostTanChuang = true;
                GameManager.Instance.TanChuangGhost();
            }
        }      
    }

    private void FixedUpdate()
    { 
        StateMachine();
        if (curState == EnemyStates.Patrol)
        {
            Move(MovementInput);
        }
        if(curState != EnemyStates.Death&&player!=null)
        {
            Shoot();
        }
    }

    private void StateMachine()
    {
        switch (curState)
        {
            case EnemyStates.Idle:

                anim.Play("null");
                MovementInput = Vector2.zero;
                rb.velocity = Vector2.zero;//����ʱ��Ҫ�ƶ�
                isPatrol = false;
                
                if (Timer <= IdleDuration)
                {
                    Timer += Time.deltaTime;
                }
                else
                {
                    Timer = 0;
                    TransState(EnemyStates.Patrol);
                   
                    
                }

                break;
            case EnemyStates.Patrol:

                anim.Play("GhostMove");

                if (isPatrol == false)
                {
                    isPatrol = true;
                    GeneratePatrolPoint();
                }

                //·�����б�Ϊ��ʱ������·������
                if (_pathPoints == null || _pathPoints.Count <= 0)
                {
                    //��������Ѳ�ߵ�
                    GeneratePatrolPoint();
                }
                else
                {
                    //�����˵��ﵱǰ·����ʱ����������currentIndex������·������
                    if (Vector2.Distance(transform.position, _pathPoints[_curIndex]) <= 0.5f)
                    {
                        _curIndex++;

                        //����Ѳ�ߵ�
                        if (_curIndex >= _pathPoints.Count)
                        {
                            TransState(EnemyStates.Idle);//�л�������״̬
                        }
                        else //δ����Ѳ�ߵ�
                        {
                            Vector2 direction = _pathPoints[_curIndex] - transform.position;
                            MovementInput = direction;  //�ƶ����򴫸�MovementInput
                        }
                    }

                    //ֹͣʱ�䵽
                    if (stopTime >= stopThreshold)
                    {
                        Debug.Log("�л�����״̬" + gameObject.name);
                        TransState(EnemyStates.Idle);//�л�Ϊ����״̬
                        stopTime = 0;
                    }

                }

                break;
            case EnemyStates.Death:

                anim.SetTrigger("Death");
                GameManager.Instance.ChangeAudioClip(GameManager.Instance.chuMo);
                GameManager.Instance.PlaySound();
                Destroy(gameObject, 1.5f);
                rb.velocity = Vector2.zero;
                isPatrol = false;

                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (curState != EnemyStates.Death && collision.CompareTag("Kinef"))
        {
            if (DataSaveManager.Instance.isChumoGhost == false)
            {
                followPlayer.GetComponent<DialogueSysYinDao>().GhostYinDao();
                DataSaveManager.Instance.isChumoGhost = true;
            }
            TransState(EnemyStates.Death);
        }
    }
    public void GetPlayerTransform()
    {
        Collider2D[] chaseColliders = Physics2D.OverlapCircleAll(transform.position, chaseDistance, playerLayer);

        if (chaseColliders.Length > 0)//�����׷����Χ��
        {
            player = chaseColliders[0].transform;//��ȡ��ҵ�Transform
            distance = Vector2.Distance(player.position, transform.position);
        }
        else
        {
            player = null;//�����׷����Χ��
        }
    }
    #region �Զ�Ѱ·
    private void AutoPath()
    {
        if (player == null)
        {
            return;
        }

        pathTimer += Time.deltaTime;
        if (pathTimer >= pathFindCooldown)
        {
            GetPathPoints(player.position);
            pathTimer = 0;
        }
        if (_pathPoints == null || _pathPoints.Count <= 0 || _pathPoints.Count <= _curIndex)
        {
            GetPathPoints(player.position);
        }
        else if (_curIndex < _pathPoints.Count)
        {
            if (Vector2.Distance(transform.position, _pathPoints[_curIndex]) <= 0.1f)
            {

                _curIndex++;
                if (_curIndex >= _pathPoints.Count)
                {
                    GetPathPoints(player.position);
                }
            }
        }
    }
    private void GetPathPoints(Vector3 target)
    {
        _curIndex = 0;

        _seeker.StartPath(transform.position, target, Path =>
        {
            _pathPoints = Path.vectorPath;
        });
    }

    #endregion

    #region �ƶ�
    public void Move(Vector2 movementInput)
    {
        if (movementInput.magnitude > 0.1f && currentSpeed >= 0)
        {

            rb.velocity = movementInput.normalized * currentSpeed;
            //�������ҷ�ת
            if (Mathf.Abs(movementInput.x) < 1)
            {
                return;
            }
            if (movementInput.x < 0)//��
            {
                sr.flipX = false;
            }
            if (movementInput.x > 0)//��
            {
                sr.flipX = true;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    #endregion

    private void TransState(EnemyStates states)
    {
        curState = states;
    }

    public void GeneratePatrolPoint()
    {
        while (true)
        {
            //���ѡ��һ��Ѳ�ߵ�����
            int i = Random.Range(0, patrolPoints.Length);

            //�ų���ǰ����
            if (targetPointIndex != i)
            {
                targetPointIndex = i;
                break;//�˳���ѭ��
            }
        }

        //��Ѳ�ߵ������·���㺯��
        GetPathPoints(patrolPoints[targetPointIndex].position);

    }

     public void Shoot()
    {
        shootTime -= Time.deltaTime;
        if (shootTime <= 0)
        {
            Instantiate(bullet, transform.position, Quaternion.identity); 
            shootTime = 2f;
        }
    }
}
