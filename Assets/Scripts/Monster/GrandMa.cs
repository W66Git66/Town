using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GrandMa : MonoBehaviour
{
    [SerializeField] private float _speed;//怪物的移动速度

    private EnemyStates curState;

    [Header("目标")]
    public Transform player;

    [Header("待机巡逻")]
    public float IdleDuration = 2f; //待机时间
    public Transform[] patrolPoints;//巡逻点
    public int targetPointIndex = 0;//目标点索引

    [Header("移动追击")]
    public float currentSpeed = 0;
    public Vector2 MovementInput { get; set; }

    public float chaseDistance = 10f;//追击距离
    public float chaseTime=2f;


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

            AutoPath();
        }

        StateMachine();
    }

    private void FixedUpdate()
    {
        if (curState == EnemyStates.Patrol)
        {
            Move(MovementInput);
        }
    }

    private void StateMachine()
    {
        switch (curState)
        {
            case EnemyStates.Idle:
                AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
                if (stateInfo.IsName("GrandMaJump")!=true)
                {
                     anim.Play("Idle");
                }
               
                MovementInput = Vector2.zero;
                rb.velocity = Vector2.zero;//待机时不要移动
                isPatrol = false;

                if (player != null)//如果玩家不为空
                {
                    Timer = 0;
                    TransState(EnemyStates.Chase);

                }
                else
                { //如果玩家为空,等待一定时间切换到巡逻状态
                    if (Timer <= IdleDuration)
                    {
                        Timer += Time.deltaTime;
                    }
                    else
                    {
                        Timer = 0;
                        TransState(EnemyStates.Patrol);
                    }
                }

                break;
            case EnemyStates.Chase:

               isPatrol = false;

                if (player == null)
                {
                    //范围外就停止追击，回到待机状态
                    Timer = 0;
                    TransState(EnemyStates.Idle);           
                }
                else
                {
                    if(Timer<chaseTime)
                    {
                        Timer += Time.deltaTime;
                    }
                    else
                    {
                        Timer = 0;
                        Vector2 playerPosition = player.position;//记录此时的位置
                        StartCoroutine(GrandMaJump(playerPosition));
                       
                       // transform.position = player.position;
                    }
                }

                break;
            case EnemyStates.Patrol:

                anim.Play("GrandMaMove");

                if (isPatrol == false)
                {
                    isPatrol = true;
                    GeneratePatrolPoint();
                }
                if (player != null)//如果玩家不为空
                {
                    TransState(EnemyStates.Chase);
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
                    if (Vector2.Distance(transform.position, _pathPoints[_curIndex]) <= 0.1f)
                    {
                        _curIndex++;

                        //到达巡逻点
                        if (_curIndex >= _pathPoints.Count)
                        {
                            Debug.Log("111");
                            TransState(EnemyStates.Idle);//切换到待机状态
                        }
                        else //未到达巡逻点
                        {
                            Vector2 direction = _pathPoints[_curIndex] - transform.position;
                            MovementInput = direction;  //移动方向传给MovementInput
                        }
                    }
                    //else
                    //{//相撞处理

                    //    //敌人刚体速度小于敌人默认的当前速度，并且敌人还未到达巡逻点
                    //    if (rb.velocity.magnitude < currentSpeed && _curIndex < _pathPoints.Count)
                    //    {
                    //        if (rb.velocity.magnitude <= currentSpeed - 0.1f)
                    //        {
                    //            if (rb.velocity.magnitude <= 0.1f)//如果敌人速度小于0.1f,在寻路范围外的敌人
                    //            {
                    //                Vector2 direction = _pathPoints[_curIndex] - transform.position;
                    //                MovementInput = direction;  //移动方向传给MovementInput
                    //            }
                    //            stopTime += Time.deltaTime;
                    //        }
                    //    }
                    //}

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

                anim.Play("null");
                MovementInput = Vector2.zero;
                rb.velocity = Vector2.zero;//待机时不要移动
                _pathPoints = null;
                isPatrol = false;

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
    }
    public void GetPlayerTransform()
    {
        Collider2D[] chaseColliders = Physics2D.OverlapCircleAll(transform.position, chaseDistance, playerLayer);

        if (chaseColliders.Length > 0)//玩家在追击范围内
        {
            curState = EnemyStates.Chase;
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
            if (movementInput.x > 0)//左
            {
                sr.flipX = false;
            }
            if (movementInput.x < 0)//右
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
        MovementInput = Vector2.zero;
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
    IEnumerator GrandMaJump(Vector2 position)
    {
        anim.Play("Idle");
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("GrandMaJump");
        transform.position = position;
    }
}
