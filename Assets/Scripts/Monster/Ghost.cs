using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Ghost : MonoBehaviour
{
    [SerializeField] private float _speed;//怪物的移动速度

    private EnemyStates curState;

    [Header("目标")]
    public Transform player;

    [Header("待机巡逻")]
    public float IdleDuration = 0.5f; //待机时间
    public Transform[] patrolPoints;//巡逻点
    public int targetPointIndex = 0;//目标点索引

    [Header("移动追击")]
    public float currentSpeed = 0;
    public Vector2 MovementInput { get; set; }

    public float chaseDistance = 3f;//追击距离


    [Header("Pathfinding")]
    private Seeker _seeker;
    private List<Vector3> _pathPoints;//路径点
    private int _curIndex;//路径点的索引
    [SerializeField] private float pathFindCooldown = 0.5f;//搜寻路径的冷却
    private float pathTimer = 0;//计时器

    [Header("攻击")]
    [HideInInspector] public float distance;
    public LayerMask playerLayer;//表示玩家图层

    private float Timer = 0;//待机计时器

    [HideInInspector] public SpriteRenderer sr;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Collider2D enemyCollider;

    private float stopTime = 0f;//敌人停止运动的时间
    private float stopThreshold = 3f;//停止的阈值

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
        enemyCollider = GetComponent<Collider2D>();//碰撞器组件
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
                rb.velocity = Vector2.zero;//待机时不要移动
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

                //路径点列表为空时，进行路径计算
                if (_pathPoints == null || _pathPoints.Count <= 0)
                {
                    //重新生成巡逻点
                    GeneratePatrolPoint();
                }
                else
                {
                    //当敌人到达当前路径点时，递增索引currentIndex并进行路径计算
                    if (Vector2.Distance(transform.position, _pathPoints[_curIndex]) <= 0.5f)
                    {
                        _curIndex++;

                        //到达巡逻点
                        if (_curIndex >= _pathPoints.Count)
                        {
                            TransState(EnemyStates.Idle);//切换到待机状态
                        }
                        else //未到达巡逻点
                        {
                            Vector2 direction = _pathPoints[_curIndex] - transform.position;
                            MovementInput = direction;  //移动方向传给MovementInput
                        }
                    }

                    //停止时间到
                    if (stopTime >= stopThreshold)
                    {
                        Debug.Log("切换待机状态" + gameObject.name);
                        TransState(EnemyStates.Idle);//切换为待机状态
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

        if (chaseColliders.Length > 0)//玩家在追击范围内
        {
            player = chaseColliders[0].transform;//获取玩家的Transform
            distance = Vector2.Distance(player.position, transform.position);
        }
        else
        {
            player = null;//玩家在追击范围外
        }
    }
    #region 自动寻路
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

    #region 移动
    public void Move(Vector2 movementInput)
    {
        if (movementInput.magnitude > 0.1f && currentSpeed >= 0)
        {

            rb.velocity = movementInput.normalized * currentSpeed;
            //敌人左右翻转
            if (Mathf.Abs(movementInput.x) < 1)
            {
                return;
            }
            if (movementInput.x < 0)//左
            {
                sr.flipX = false;
            }
            if (movementInput.x > 0)//右
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
            //随机选择一个巡逻点索引
            int i = Random.Range(0, patrolPoints.Length);

            //排除当前索引
            if (targetPointIndex != i)
            {
                targetPointIndex = i;
                break;//退出死循环
            }
        }

        //把巡逻点给生成路径点函数
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
