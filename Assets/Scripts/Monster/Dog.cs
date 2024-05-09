using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Dog : MonoBehaviour
{
    [SerializeField] private float _speed;//怪物的移动速度

    private EnemyStates curState;

    [Header("目标")]
    public Transform player;

    [Header("待机巡逻")]
    public float IdleDuration; //待机时间
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
    [SerializeField]private float pathFindCooldown=0.5f;//搜寻路径的冷却
    private float pathTimer = 0;//计时器

    [Header("攻击")]
    [HideInInspector] public float distance;
    public LayerMask playerLayer;//表示玩家图层

    private float Timer = 0;//待机计时器

    [HideInInspector] public SpriteRenderer sr;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Collider2D enemyCollider;

    private void Awake()
    {
        _seeker=GetComponent<Seeker>();

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
        GetPlayerTransform();

        AutoPath();

        StateMachine();
    }

    private void FixedUpdate()
    {
        if(curState==EnemyStates.Chase)
        {
            Move(MovementInput);
        }
    }

    private void StateMachine()
    {
        switch(curState)
        {
            case EnemyStates.Idle :

                anim.Play("null");
                rb.velocity = Vector2.zero;//待机时不要移动

                if (player != null)//如果玩家不为空
                {

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

                anim.Play("DogMove");

                if (player != null)
                {
                    //判定路径点列表是否为空
                    if (_pathPoints == null || _pathPoints.Count <= 0)
                        return;             
                        //追逐玩家
                        Vector2 direction = _pathPoints[_curIndex] - transform.position;
                        MovementInput = direction;//移动方向传给MovementInput
                }
                else
                {
                    //范围外就停止追击，回到待机状态
                    TransState(EnemyStates.Idle);
                }
                break;
            case EnemyStates.Patrol:


                break;
            case EnemyStates.Death:
                
                
                break;
        }    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(curState!=EnemyStates.Death&&collision.CompareTag("蛋白粉"))
        {
            curState = EnemyStates.Death;
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
        if(pathTimer >= pathFindCooldown) 
        {
            GetPathPoints(player.position);
            pathTimer = 0;
        }
        if (_pathPoints == null||_pathPoints.Count<=0||_pathPoints.Count <= _curIndex)
        {
            GetPathPoints(player.position);
        }
        else if(_curIndex<_pathPoints.Count)
        {
            if (Vector2.Distance(transform.position, _pathPoints[_curIndex]) <= 0.1f)
            {

                _curIndex++;
                if( _curIndex >= _pathPoints.Count )
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
            if(Mathf.Abs(movementInput.x)<1)
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
}
